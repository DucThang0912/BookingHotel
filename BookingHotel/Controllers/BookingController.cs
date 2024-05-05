using System.Linq;
using System.Threading.Tasks;
using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> HistoryAsync()
        {
            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.GetUserAsync(HttpContext.User);

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (user != null)
            {
                // Truy vấn các đơn hàng của người dùng dựa trên UserId
                var bookings = await _context.Bookings
                    .Where(o => o.UserId == user.Id)
                    .ToListAsync();

                return View(bookings);
            }
            else
            {
                // Người dùng chưa đăng nhập, có thể xử lý tùy ý
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
