namespace BookingHotel.Models
{
    public class RoomAmenity
    {
        public int Id { get; set; }
        public int? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public int? AmenityId { get; set; }
        public Amenity? Amenity { get; set; }
    }
}
