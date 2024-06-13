using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingByUserId(string userId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync();
        Task<Dictionary<string, decimal>> GetHotelRevenueAsync(int hotelId);
    }
}
