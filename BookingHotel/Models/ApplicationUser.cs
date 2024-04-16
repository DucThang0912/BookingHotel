using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
