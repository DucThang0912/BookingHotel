using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFBookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Booking>> GetBookingByUserId(string userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.Include(b => b.BookingDetails).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Where(b => b.CreatedAt >= startDate && b.CreatedAt <= endDate)
                .Include(b => b.BookingDetails)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.BookingDetails)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync()
        {
            return await Task.Run(() => _context.Bookings
                .AsEnumerable()  // Chuyển đổi dữ liệu thành kiểu liệt kê
                .GroupBy(b => b.CreatedAt.ToString("yyyy-MM"))
                .Select(g => new { Month = g.Key, Revenue = g.Sum(b => b.TotalAmount) })
                .ToDictionary(g => g.Month, g => g.Revenue));
        }

        public async Task<Dictionary<string, decimal>> GetHotelRevenueAsync(int hotelId)
        {
            return await Task.Run(() => _context.Bookings
                .Include(b => b.BookingDetails)
                    .ThenInclude(d => d.roomType)
                .AsEnumerable()  // Chuyển đổi dữ liệu thành kiểu liệt kê
                .Where(b => b.BookingDetails.Any(d => d.roomType.HotelId == hotelId))
                .GroupBy(b => b.CreatedAt.ToString("yyyy-MM"))
                .Select(g => new { Month = g.Key, Revenue = g.Sum(b => b.TotalAmount) })
                .ToDictionary(g => g.Month, g => g.Revenue));
        }
    }
}
