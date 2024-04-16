using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
        public string OrderName { get; set; }
        public string PhoneNumber { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}

