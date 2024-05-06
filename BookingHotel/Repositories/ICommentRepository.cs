using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<List<Comment>> GetCommentsByUserIdAsync(string userId);
        Task<List<Comment>> GetCommentsByHotelIdAsync(int hotelId);
        Task AddCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsByHotelIdAsync(int hotelId);
    }
}
