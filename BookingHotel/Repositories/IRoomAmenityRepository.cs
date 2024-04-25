using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IRoomAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAmenitiesAsync();
        Task<IEnumerable<int>> GetAmenitiesByRoomIdAsync(int roomId);
        Task AddAsync(RoomAmenity roomAmenity);
        Task DeleteAsync(int roomId, int amenityId);
        Task<RoomAmenity> GetAmenityByRoomIdAndAmenityIdAsync(int roomId, int amenityId);
    }
}
