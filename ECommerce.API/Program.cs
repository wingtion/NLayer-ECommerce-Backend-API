using ECommerce.API.Filters;
using ECommerce.Core;
using ECommerce.Core.Configuration;
using ECommerce.Core.Repositories;
using ECommerce.Core.Services;
using ECommerce.Core.UnitOfWorks;
using ECommerce.Repository;
using ECommerce.Repository.Repositories;
using ECommerce.Repository.UnitOfWorks;
using ECommerce.Service.Mapping;
using ECommerce.Service.Services;
using ECommerce.Service.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Ayarlarý
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

// 2. Identity (Kullanýcý Yönetimi) Ayarlarý
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; // Ayný email ile 2 kiþi kayýt olamasýn
    options.Password.RequireNonAlphanumeric = false; // Sembol zorunluluðu olmasýn (Test kolay olsun diye)
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 3. AppSettings'den Token Ayarlarýný Okuma
// "TokenOption" kýsmýný sýnýfa baðlýyoruz (Map ediyoruz)
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// 4. JWT Authentication Ayarlarý (Token Doðrulama)
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
        ClockSkew = TimeSpan.Zero // Süre bitince hemen hata ver (Normalde 5 dk tolerans vardýr)
    };
});

// 5. Dependency Injection (DI) Tanýmlarý
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

// Auth servislerini ekliyoruz
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();

// 6. Controller & Validation
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute());
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

// 7. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });

    // Swagger'a "Burada bir güvenlik kilidi var" diyoruz
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token'ý buraya yapýþtýrýn. Örnek: Bearer eyJhbGciOiJIUz..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// SIRALAMA ÇOK ÖNEMLÝ: Önce Kimlik Doðrulama (Authentication), Sonra Yetki Kontrolü (Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();