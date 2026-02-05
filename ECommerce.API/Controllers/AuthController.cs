using ECommerce.Core.DTOs;
using ECommerce.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly AuthService _authenticationService;

        public AuthController(AuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // Giriş Yapma Ucu
        [HttpPost("login")]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            return CreateActionResult(result);
        }

        // Kayıt Olma Ucu
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _authenticationService.CreateUserAsync(createUserDto);
            return CreateActionResult(result);
        }
    }
}