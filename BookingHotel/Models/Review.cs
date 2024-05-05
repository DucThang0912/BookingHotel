namespace BookingHotel.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; } 
    }
}

