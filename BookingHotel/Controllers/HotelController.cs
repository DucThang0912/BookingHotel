using Microsoft.AspNetCore.Mvc;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookingHotel.Models;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IRegionRepository regionRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            foreach (var hotel in hotels)
            {
                if (hotel.RegionId != null)
                {
                    hotel.Region = await _regionRepository.GetByIdAsync(hotel.RegionId);
                }
            }
            return View(hotels);
        }
        
    }
}
