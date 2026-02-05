using Microsoft.AspNetCore.Identity;

namespace ECommerce.Core
{
    // IdentityUser sınıfından miras alıyoruz. 
    // Bu sayede Id, UserName, Email, PasswordHash gibi alanlar OTOMATİK gelecek.
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}