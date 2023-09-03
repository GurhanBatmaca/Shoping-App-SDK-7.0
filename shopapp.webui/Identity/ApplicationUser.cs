using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}