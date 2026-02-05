using AutoMapper;
using ECommerce.Core;
using ECommerce.Core.DTOs;

namespace ECommerce.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // --- PRODUCT MAPPING ---
            // Veritabanından okurken (Entity -> DTO): Her şeyi olduğu gibi al.
            CreateMap<Product, ProductDto>();

            // Veritabanına kaydederken (DTO -> Entity): CreatedDate alanını GÖRMEZDEN GEL (Ignore).
            // Böylece veritabanı kendi varsayılan zamanını (DateTime.Now) kullanabilir.
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());


            // --- CATEGORY MAPPING ---
            // Entity -> DTO
            CreateMap<Category, CategoryDto>();

            // DTO -> Entity (Tarihi görmezden gel)
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());


            // --- USER MAPPING ---
            // Kullanıcı işlemlerinde tarih sorunu kritik olmadığı için ReverseMap kalabilir.
            CreateMap<AppUser, UserAppDto>().ReverseMap();

            // Yeni kayıt olurken
            CreateMap<CreateUserDto, AppUser>();
        }
    }
}