namespace BookingHotel.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public ICollection<HotelService>? HotelServices { get; set; }
    }
}
