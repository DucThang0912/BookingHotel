using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFAmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<Amenity> GetByIdAsync(int id)
        {
            return await _context.Amenities.FindAsync(id);
        }
        public async Task AddAsync(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
