using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BookingHotel.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public string Name { get; set; }
        public int TotalGuests { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<RoomTypeImage>? Images { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương.")]
        public decimal PricePerNight { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}

