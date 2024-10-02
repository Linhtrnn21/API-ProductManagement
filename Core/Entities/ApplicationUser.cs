using Microsoft.AspNetCore.Identity;

namespace PM.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Role { get; set; }
    }
}
