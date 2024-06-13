using Microsoft.AspNetCore.Mvc;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookingHotel.Models;
using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookingHotel.Data;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomAmenityRepository _roomAmenityRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public HotelController(IHotelRepository hotelRepository, IHotelServiceRepository hotelServiceRepository, IServiceRepository serviceRepository, IRoomRepository roomRepository, IRoomAmenityRepository roomAmenityRepository, IRegionRepository regionRepository, IRoomTypeRepository roomTypeRepository, IReviewRepository reviewRepository, ICommentRepository commentRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _hotelRepository = hotelRepository;
            _hotelServiceRepository = hotelServiceRepository;
            _serviceRepository = serviceRepository;
            _roomRepository = roomRepository;
            _roomAmenityRepository = roomAmenityRepository;
            _regionRepository = regionRepository;
            _roomTypeRepository = roomTypeRepository;
            _reviewRepository = reviewRepository;
            _commentRepository = commentRepository;
            _userManager = userManager;
            _context = context;
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
        public async Task<IActionResult> Display(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            var hotelImages = await _hotelRepository.GetHotelImagesAsync(id);
            var roomTypes = await _roomTypeRepository.GetRoomTypesByHotelIdAsync(id);
            var amenities = new Dictionary<int, ICollection<RoomAmenity>>();
            var hotelForRegionByHotelId = await _hotelRepository.GetHotelForRegionByHotelId(id, hotel.RegionId);

            foreach (var roomType in roomTypes)
            {
                var roomAmenities = await _roomAmenityRepository.GetRoomAmenitiesByRoomTypeIdAsync(roomType.Id);
                amenities.Add(roomType.Id, roomAmenities);
            }

            var service = await _serviceRepository.GetAllAsync();
            var selectedServices = await _hotelServiceRepository.GetListServicesByHotelIdAsync(id);

            hotel.Images = hotelImages;
            hotel.RoomTypes = roomTypes.ToList();

            // Lấy số lượng phòng của mỗi loại phòng
            var roomCounts = new Dictionary<int, int>();
            foreach (var roomType in hotel.RoomTypes)
            {
                int count = await _roomTypeRepository.CountRoomByRoomTypeAsync(roomType.Id);
                roomCounts.Add(roomType.Id, count);
            }
            // Lấy review và danh sách comment của khách sạn
            var reviews = await _reviewRepository.GetAllReviewsByHotelIdAsync(id);
            var comments = await _commentRepository.GetCommentsByHotelIdAsync(id);
            hotel.Reviews = reviews;

            // Truyền dữ liệu sang view
            ViewBag.HotelForRegionByHotelId = hotelForRegionByHotelId;
            ViewBag.SelectedServices = selectedServices;
            ViewBag.Service = service;
            ViewData["Comments"] = comments;
            ViewData["RoomCounts"] = roomCounts; 
            ViewData["Amenities"] = amenities;  
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                ViewBag.FullName = user.FullName;
                bool checkReviewOfUser = await _reviewRepository.CheckReviewOfUserAsync(user.Id, id);
                ViewBag.CheckReviewOfUser = checkReviewOfUser;
            }
            ViewBag.UserManager = _userManager;
            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int hotelId, int service, int facilities, int cleanliness, int comfort, int location)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }
            // Lấy thông tin người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tạo đánh giá mới
            var review = new Review
            {
                HotelId = hotelId,
                UserId = userId,
                Service = service,
                Facilities = facilities,
                Cleanliness = cleanliness,
                Comfort = comfort,
                Location = location
            };

            await _reviewRepository.AddReviewAsync(review);

            // Chuyển hướng về trang chi tiết hotel
            return RedirectToAction("Display", new { id = hotelId });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int hotelId, string content)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }
            // Lấy thông tin người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
            // Tạo comment mới
            var newComment = new Comment
            {
                HotelId = hotelId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.Now
            };

            // Lưu comment vào cơ sở dữ liệu
            await _commentRepository.AddCommentAsync(newComment);

            // Chuyển hướng về trang chi tiết hotel
            return RedirectToAction("Display", new { id = hotelId });
        }

        public IActionResult FilterRooms(DateTime checkInDate, DateTime checkOutDate, int adults, int children, int roomId)
        {
            // Lọc danh sách phòng dựa trên các điều kiện lọc
            var filteredRooms = _roomTypeRepository.FilterRooms(checkInDate, checkOutDate, adults, children, roomId);

            // Trả về danh sách phòng đã lọc dưới dạng partial view
            return PartialView("_RoomListPartial", filteredRooms);
        }

    }
}
