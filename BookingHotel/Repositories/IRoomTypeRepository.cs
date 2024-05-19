using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task<RoomType> GetByIdAsync(int id);
        Task<IEnumerable<RoomType>> GetRoomTypesByHotelIdAsync(int hotelId);
        Task AddAsync(RoomType roomType);
        Task AddRoomTypeImageAsync(RoomTypeImage roomTypeImage);
        Task <List<RoomTypeImage>> GetRoomTypeImagesAsync(int id);
        Task<List<RoomTypeImage>> GetRoomTypeImagesByRoomTypeIdAsync(int roomTypeId);
        Task UpdateAsync(RoomType roomType);
        Task DeleteAsync(int id);
        Task<int> CountRoomByRoomTypeAsync(int id);
        IEnumerable<RoomType> FilterRooms(DateTime checkInDate, DateTime checkOutDate, int adults, int children, int hotelId);
    }
}
