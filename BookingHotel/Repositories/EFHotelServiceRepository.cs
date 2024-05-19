using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookingHotel.Repositories
{
    public class EFHotelServiceRepository : IHotelServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public EFHotelServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<IEnumerable<int>> GetServicesByHotelIdAsync(int hotelId)
        {
            var serviceIds = await _context.HotelServices
                .Where(hs => hs.HotelId == hotelId && hs.ServiceId != null)
                .Select(hs => hs.ServiceId.Value) // Use Value property to get non-nullable integer
                .ToListAsync();

            return serviceIds;
        }

        public async Task<IEnumerable<Service>> GetListServicesByHotelIdAsync(int hotelId)
        {
            return await _context.HotelServices
                .Where(hs => hs.HotelId == hotelId)
                .Select(hs => hs.Service)
                .ToListAsync();
        }

        public async Task AddAsync(HotelService hotelService)
        {
            _context.HotelServices.Add(hotelService);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int hotelId, int serviceId)
        {
            var hotelService = await _context.HotelServices.FirstOrDefaultAsync(hs => hs.HotelId == hotelId && hs.ServiceId == serviceId);
            if (hotelService != null)
            {
                _context.HotelServices.Remove(hotelService);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelService> GetServiceByHotelIdAndServiceIdAsync(int hotelId, int serviceId)
        {
            return await _context.HotelServices
                .FirstOrDefaultAsync(hs => hs.HotelId == hotelId && hs.ServiceId == serviceId);
        }
    }
}
