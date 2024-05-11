namespace BookingHotel.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HotelService>? HotelServices { get; set; }
    }
}
