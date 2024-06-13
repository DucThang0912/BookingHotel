using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task<Review> GetReviewByHotelIdAsync(int hotelId);
        Task<List<Review>> GetAllReviewsByHotelIdAsync(int hotelId);
        Task<bool> CheckReviewOfUserAsync(string userId, int hotelId);
    }
}
