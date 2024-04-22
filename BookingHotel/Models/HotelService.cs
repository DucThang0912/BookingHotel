namespace BookingHotel.Models
{
    public class HotelService
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
