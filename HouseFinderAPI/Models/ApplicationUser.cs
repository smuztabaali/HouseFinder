using Microsoft.AspNetCore.Identity;

namespace HouseFinderAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
