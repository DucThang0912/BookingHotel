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
        // Đánh giá về các tiêu chí
        public int Service { get; set; } // Nhân viên phục vụ
        public int Facilities { get; set; } // Tiện nghi
        public int Cleanliness { get; set; } // Sạch sẽ
        public int Comfort { get; set; } // Thoải mái
        public int Location { get; set; } // Địa điểm

        // tính toán điểm trung bình
        public double CalculateAverageRating()
        {
            int totalRating = Service + Facilities + Cleanliness + Comfort + Location;
            return (double)totalRating / 5;
        }
    }
}
