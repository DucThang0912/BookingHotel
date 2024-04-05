namespace BookingHotel.Models
{
    public class HotelImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
        public Hotel? hotel { get; set; }

    }
}


