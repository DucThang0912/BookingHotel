using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFRoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRoomTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoomType>> GetAllAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        public async Task<RoomType> GetByIdAsync(int id)
        {
            return await _context.RoomTypes.FindAsync(id);
        }
        public async Task AddAsync(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<RoomType>> GetRoomTypesByHotelIdAsync(int hotelId)
        {
            IQueryable<RoomType> query = _context.RoomTypes.Where(r => r.HotelId == hotelId);
            return await query.ToListAsync();
        }
        public async Task AddRoomTypeImageAsync(RoomTypeImage roomTypeImage)
        {
            _context.RoomTypeImages.Add(roomTypeImage);
            await _context.SaveChangesAsync();
        }
        public async Task<List<RoomTypeImage>> GetRoomTypeImagesAsync(int id)
        {
            return await _context.RoomTypeImages.Where(rti => rti.Id == id).ToListAsync();
        }
        public async Task UpdateAsync(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountRoomByRoomTypeAsync(int id)
        {
            int count = await _context.Rooms
                .Where(r => r.RoomType.Id == id && r.IsAvailable == false)
                .CountAsync();

            return count;
        }

    }
}
