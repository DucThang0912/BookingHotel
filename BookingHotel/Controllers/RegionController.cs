using BookingHotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        public RegionController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IRegionRepository regionRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var regions = await _regionRepository.GetAllAsync();
            return View(regions);
        }
    }
}
