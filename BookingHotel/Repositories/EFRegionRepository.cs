using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class EFRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(int id)
        {
            return await _context.Regions.FindAsync(id);
        }
        public async Task AddAsync(Region region)
        {
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Region region)
        {
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
        }
    }
}
