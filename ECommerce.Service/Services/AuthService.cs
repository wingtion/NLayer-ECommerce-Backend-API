using AutoMapper;
using ECommerce.Core;
using ECommerce.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Service.Services
{
    public class AuthService
    {
        private readonly TokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper; // Mapper eklendi

        public AuthService(TokenService tokenService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
        }

        // --- LOGIN ---
        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong");

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkPassword) return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong");

            var token = _tokenService.CreateToken(user);
            return CustomResponseDto<TokenDto>.Success(200, token);
        }

        // --- REGISTER (YENİ EKLENDİ) ---
        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new AppUser
            {
                Email = createUserDto.Email,
                UserName = createUserDto.Email, // Kullanıcı adı email olsun
                FullName = createUserDto.FullName
            };

            // CreateAsync metodu şifreyi otomatik Hash'ler (Kriptolar)
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(400, errors);
            }

            // Geriye UserAppDto dönüyoruz (Şifresiz hali)
            return CustomResponseDto<UserAppDto>.Success(200, _mapper.Map<UserAppDto>(user));
        }
    }
}