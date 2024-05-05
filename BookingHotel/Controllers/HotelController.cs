using Microsoft.AspNetCore.Mvc;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookingHotel.Models;
using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IRegionRepository regionRepository, IRoomTypeRepository roomTypeRepository, IReviewRepository reviewRepository, UserManager<ApplicationUser> userManager)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _regionRepository = regionRepository;
            _roomTypeRepository = roomTypeRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            //foreach (var hotel in hotels)
            //{
            //    if (hotel.RegionId != null)
            //    {
            //        hotel.Region = await _regionRepository.GetByIdAsync(hotel.RegionId);
            //    }
            //}
            return View(hotels);
        }
        public async Task<IActionResult> Display(int id)
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            var hotel = await _hotelRepository.GetByIdAsync(id);
            var hotelImages = await _hotelRepository.GetHotelImagesAsync(id);

            // Lấy danh sách phòng và bao gồm thông tin RoomType của mỗi phòng
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(id, includeRoomType: true);
            hotel.Images = hotelImages;
            hotel.Rooms = rooms.ToList();
            hotel.Reviews = reviews;
            var user = await _userManager.GetUserAsync(User);
            ViewBag.FullName = user.FullName;
            // Truyền dữ liệu hotel sang view
            return View(hotel);
        }

        public async Task<IActionResult> AddComment(int hotelId, string content)
        {
            // Lấy thông tin người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tạo đánh giá mới
            var newReview = new Review
            {
                HotelId = hotelId,
                UserId = userId,
                Comment = content,
                CreatedAt = DateTime.Now
            };

            // Lưu đánh giá vào cơ sở dữ liệu
            await _reviewRepository.AddReviewAsync(newReview);

            // Chuyển hướng về trang chi tiết hotel
            return RedirectToAction("Display", new { id = hotelId });
        }

    }
}
