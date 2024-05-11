using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace BookingHotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public Boolean? IsAvailable { get; set; }
        public ICollection<BookingRoom>? BookingRooms { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
    }
}

