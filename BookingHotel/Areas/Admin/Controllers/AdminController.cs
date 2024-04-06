using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityUser> _roleManager;
        //public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityUser> roleManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}
        private readonly IHotelRepository _hotelRepository;
        private readonly IRegionRepository _regionRepository;
        public AdminController(IHotelRepository hotelRepository, IRegionRepository regionRepository)
        {
            _hotelRepository = hotelRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            return View(hotels);
        }
    }
}
