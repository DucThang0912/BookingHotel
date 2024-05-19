namespace BookingHotel.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int TotalGuests { get; set; }
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
