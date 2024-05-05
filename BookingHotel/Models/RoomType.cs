namespace BookingHotel.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? Description { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}

