using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId, bool includeRoomType = false);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
