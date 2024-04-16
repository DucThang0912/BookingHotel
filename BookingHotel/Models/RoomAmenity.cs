namespace BookingHotel.Models
{
    public class RoomAmenity
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public Room? Rooms { get; set; }
        public int? AmenityId { get; set; }
        public Amenity? Amenities { get; set; }
    }
}
