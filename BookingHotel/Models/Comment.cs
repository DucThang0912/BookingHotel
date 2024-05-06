namespace BookingHotel.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // Mã nhà khách sạn liên kết với đánh giá
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        // ID của người dùng tạo đánh giá
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
