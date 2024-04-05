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
    }
}
