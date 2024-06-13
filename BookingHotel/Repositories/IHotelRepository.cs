using BookingHotel.Models;

namespace BookingHotel.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetByIdAsync(int id);
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
        Task AddHotelImageAsync(HotelImage hotelImage);
        Task <List<HotelImage>> GetHotelImagesAsync(int hotelId);
        void RemoveHotelImages(int hotelId);
        Task<IEnumerable<Hotel>> GetHotelForRegionByHotelId(int id, int regionId);
    }
}
