using ECommerce.Core.Configuration;
using ECommerce.Core.DTOs;
using ECommerce.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace ECommerce.Service.Services
{
    // Bu sınıfın Interface'ini (ITokenService) yazmadık, direkt kullanacağız şimdilik.
    public class TokenService
    {
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IOptions<CustomTokenOption> options)
        {
            _tokenOption = options.Value;
        }

        public TokenDto CreateToken(AppUser user)
        {
            // Token'ın geçerlilik süresi
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            // Token'ın içine gömülecek bilgiler (Claims)
            // Token çalınsa bile şifre vs. görünmez ama Id, Email görünür.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // İmzalama anahtarı
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            // Şifreleme algoritması (HS256)
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Token'ı oluştur
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,

                audience: _tokenOption.Audience[0],

                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            // DTO olarak geri dön
            return new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration)
            };
        }

        // Basit bir rastgele string üretici (Refresh Token için)
        public string CreateRefreshToken()
        {
            var number = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}