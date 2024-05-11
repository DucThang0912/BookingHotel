using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IRoomAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAmenitiesAsync();
        Task<IEnumerable<int>> GetAmenitiesByRoomIdAsync(int roomTypeId);
        Task AddAsync(RoomAmenity roomAmenity);
        Task DeleteAsync(int roomTypeId, int amenityId);
        Task<RoomAmenity> GetAmenityByRoomIdAndAmenityIdAsync(int roomTypeId, int amenityId);
    }
}
