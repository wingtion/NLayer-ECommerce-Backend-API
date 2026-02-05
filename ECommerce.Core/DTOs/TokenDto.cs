namespace ECommerce.Core.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; } // İleride lazım olur
        public DateTime RefreshTokenExpiration { get; set; }
    }
}