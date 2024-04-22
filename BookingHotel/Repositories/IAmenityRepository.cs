using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetByIdAsync(int id);
        Task AddAsync(Amenity amenity);
        Task UpdateAsync(Amenity amenity);
        Task DeleteAsync(int id);
    }
}
