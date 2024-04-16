namespace BookingHotel.Models
{
    public class HotelService
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public Hotel? Hotels { get; set; }
        public int? ServiceId { get; set; }
        public Service? Services { get; set; }
    }
}
