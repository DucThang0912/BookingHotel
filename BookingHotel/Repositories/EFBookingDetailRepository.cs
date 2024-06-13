using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFBookingDetailRepository : IBookingDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBookingDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDetail>> GetAllBookingDetailAsync()
        {
            return await _context.BookingsDetail.ToListAsync();
        }
        public async Task<List<BookingDetail>> GetBookingDetailByBookingId(int bookingId)
        {
            return await _context.BookingsDetail.Where(bd => bd.BookingId == bookingId).ToListAsync();
        }
    }
}
