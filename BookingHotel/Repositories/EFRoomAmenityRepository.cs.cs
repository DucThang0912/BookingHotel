using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFRoomAmenityRepository : IRoomAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRoomAmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAmenitiesAsync()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAmenitiesByRoomIdAsync(int roomTypeId)
        {
            var amenityIds = await _context.RoomAmenities
                .Where(ra => ra.RoomTypeId == roomTypeId && ra.AmenityId != null)
                .Select(ra => ra.AmenityId.Value)
                .ToListAsync();

            return amenityIds;
        }
        public async Task AddAsync(RoomAmenity roomAmenity)
        {
            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int roomTypeId, int amenityId)
        {
            var roomAmenity = await _context.RoomAmenities.FirstOrDefaultAsync(ra => ra.RoomTypeId == roomTypeId && ra.AmenityId == amenityId);
            if (roomAmenity != null)
            {
                _context.RoomAmenities.Remove(roomAmenity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomAmenity> GetAmenityByRoomIdAndAmenityIdAsync(int roomTypeId, int amenityId)
        {
            return await _context.RoomAmenities
                .FirstOrDefaultAsync(ra => ra.RoomTypeId == roomTypeId && ra.AmenityId == amenityId);
        }
    }
}
