using System.ComponentModel.DataAnnotations;
namespace BookingHotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public long PricePerNight { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}

