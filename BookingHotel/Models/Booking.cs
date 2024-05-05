using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string OrderName { get; set; }
        public string PhoneNumber { get; set; } 
        public string? Notes { get; set; }
        public bool IsConfirmed { get; set; } 
        public ICollection<BookingRoom> BookingRooms { get; set; }

    }
}

