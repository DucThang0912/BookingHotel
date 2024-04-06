using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRegionRepository _regionRepository;
        
        public HotelController(IHotelRepository hotelRepository, IRegionRepository regionRepository)
        {
            _hotelRepository = hotelRepository;
            _regionRepository = regionRepository;
        }
        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            foreach (var hotel in hotels)
            {
                if(hotel.RegionId != null)
                {
                    hotel.Region = await _regionRepository.GetByIdAsync(hotel.RegionId);
                }
            }
            return View();
        }


    }
}
