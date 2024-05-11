namespace BookingHotel.Models
{
    public class RoomTypeImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
    }
}
