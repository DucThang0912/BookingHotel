using Microsoft.EntityFrameworkCore;
using BookingHotel.Repositories;
using BookingHotel.Data;
using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public class EFHotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public EFHotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }
        public async Task AddAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task AddHotelImageAsync(HotelImage hotelImage)
        {
            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();
        }
        public async Task<List<HotelImage>> GetHotelImagesAsync(int hotelId)
        {
            return await _context.HotelImages.Where(hi => hi.HotelId == hotelId).ToListAsync();
        }
        public void RemoveHotelImages(int hotelId)
        {
            var hotelImages = _context.HotelImages.Where(hi => hi.HotelId == hotelId);
            _context.HotelImages.RemoveRange(hotelImages);
            _context.SaveChanges();
        }
    }
}
