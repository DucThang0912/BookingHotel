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
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal PricePerNight { get; set; }
        public Boolean? IsAvailable { get; set; }
        public ICollection<BookingRoom> BookingRooms { get; set; }
        public ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}

