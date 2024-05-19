using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public IActionResult BookingNow(string bookingViews)
        {
            // Giải mã dữ liệu từ URL
            var decodedData = HttpUtility.UrlDecode(bookingViews);

            // Chuyển dữ liệu từ chuỗi JSON thành danh sách BookingView
            var bookingViewsList = JsonConvert.DeserializeObject<List<BookingView>>(decodedData);

            if (bookingViewsList != null && bookingViewsList.Count > 0)
            {
                ViewBag.BookingViews = bookingViewsList;
            }
            else
            {
                return RedirectToAction("Error");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookingNow(BookingViewModel model, string bookingViews)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var booking = new Booking
            {
                UserId = user.Id,
                User = user,
                firstName = model.Booking.firstName,
                LastName = model.Booking.LastName,
                Email = model.Booking.Email,
                PhoneNumber = model.Booking.PhoneNumber,
                Notes = model.Booking.Notes,
                PaymentMethod = model.Booking.PaymentMethod,
                CreatedAt = DateTime.Now,
                IsConfirmed = false
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Deserialize bookingViews
            var bookingViewsList = JsonConvert.DeserializeObject<List<BookingView>>(bookingViews);

            if (bookingViewsList != null)
            {
                booking.BookingDetails = new List<BookingDetail>();
                foreach (var bookingView in bookingViewsList)
                {
                    var bookingDetail = new BookingDetail
                    {
                        BookingId = booking.Id,
                        RoomTypeId = bookingView.RoomTypeId,
                        Quantity = bookingView.Quantity,
                        Adults = bookingView.Adults,
                        Children = bookingView.Children,
                        CheckInDate = bookingView.CheckInDate,
                        CheckOutDate = bookingView.CheckOutDate,
                        Total = bookingView.TotalPrice
                    };
                    booking.BookingDetails.Add(bookingDetail);
                }
            }

            booking.TotalAmount = booking.BookingDetails.Sum(bd => bd.Total);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BookingConfirmation));
        }

        public IActionResult BookingConfirmation()
        {
            return View();
        }
    }
}
