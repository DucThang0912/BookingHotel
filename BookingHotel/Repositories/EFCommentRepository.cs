using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFCommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByUserIdAsync(string userId)
        {
            return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByHotelIdAsync(int hotelId)
        {
            return await _context.Comments.Where(c => c.HotelId == hotelId).ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Comment>> GetAllCommentsByHotelIdAsync(int hotelId)
        {
            return await _context.Comments.Where(c => c.HotelId == hotelId).ToListAsync();
        }
    }
}
