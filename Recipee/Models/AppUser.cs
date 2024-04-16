using Microsoft.AspNetCore.Identity;

namespace Recipee.Models
{
    public class AppUser : IdentityUser
    {

        public bool IsAdmin { get; set; } = false;

    }
}
