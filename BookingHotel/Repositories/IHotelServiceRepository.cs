using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IHotelServiceRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<IEnumerable<int>> GetServicesByHotelIdAsync(int hotelId);
        Task<IEnumerable<Service>> GetListServicesByHotelIdAsync(int hotelId);
        Task AddAsync(HotelService hotelService);
        Task DeleteAsync(int hotelId, int serviceId);
        Task<HotelService> GetServiceByHotelIdAndServiceIdAsync(int hotelId, int serviceId);
    }
}
