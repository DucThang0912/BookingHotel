using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IBookingDetailRepository
    {
        Task<List<BookingDetail>> GetAllBookingDetailAsync();
        Task<List<BookingDetail>> GetBookingDetailByBookingId(int bookingId);
    }
}
