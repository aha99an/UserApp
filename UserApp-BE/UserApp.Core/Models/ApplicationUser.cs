using Microsoft.AspNetCore.Identity;

namespace UserApp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
