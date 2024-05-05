namespace BookingHotel.Models
{
    public class Review
    {
        public int Id { get; set; }

        // Mã nhà khách sạn liên kết với đánh giá
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        // ID của người dùng tạo đánh giá
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // Nội dung bình luận
        public string? Comment { get; set; }

        // Điểm đánh giá
        public int? Rating { get; set; }

        // Thời gian tạo và cập nhật
        public DateTime CreatedAt { get; set; }
    }
}
