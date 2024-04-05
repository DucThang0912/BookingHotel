namespace BookingHotel.Models
{
    public class RoomImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int RoomId { get; set; }
        public Room? room { get; set; }

    }
}

