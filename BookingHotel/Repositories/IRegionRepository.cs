using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(int id);
        Task AddAsync(Region region);
        Task UpdateAsync(Region region);
        Task DeleteAsync(int id);
    }
}
