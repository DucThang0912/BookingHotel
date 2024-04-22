namespace BookingHotel.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}

