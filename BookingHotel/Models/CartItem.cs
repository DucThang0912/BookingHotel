namespace BookingHotel.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
