using BookingHotel.Data;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;

        public StatisticsController(ApplicationDbContext context, IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> InteractionStatistics()
        {
            return View();
        }

        public async Task<IActionResult> BookingStatistics()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> RevenueStatistics()
        {
            var monthlyRevenue = await _bookingRepository.GetMonthlyRevenueAsync();
            ViewBag.MonthlyRevenue = monthlyRevenue;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HotelRevenueStatistics(int hotelId)
        {
            var monthlyRevenue = await _bookingRepository.GetMonthlyRevenueAsync();
            ViewBag.MonthlyRevenue = monthlyRevenue;
            var hotelRevenue = await _bookingRepository.GetHotelRevenueAsync(hotelId);
            ViewBag.HotelRevenue = hotelRevenue;
            return View("RevenueStatistics");
        }

    }
}
