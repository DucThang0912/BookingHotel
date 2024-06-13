using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public EFReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task<Review> GetReviewByHotelIdAsync(int hotelId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.HotelId == hotelId);
        }
        public async Task<List<Review>> GetAllReviewsByHotelIdAsync(int hotelId)
        {
            return await _context.Reviews.Where(r => r.HotelId == hotelId).ToListAsync();
        }
        public async Task<bool> CheckReviewOfUserAsync(string userId, int hotelId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.HotelId == hotelId);
        }
    }
}
