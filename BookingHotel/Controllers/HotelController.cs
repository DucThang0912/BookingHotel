﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IRegionRepository regionRepository, IRoomTypeRepository roomTypeRepository, IReviewRepository reviewRepository, ICommentRepository commentRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
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
            var hotel = await _hotelRepository.GetByIdAsync(id);
            var hotelImages = await _hotelRepository.GetHotelImagesAsync(id);
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(id, includeRoomType: true);
            hotel.Images = hotelImages;
            hotel.Rooms = rooms.ToList();

            // Lấy review và danh sách comment của khách sạn
            var review = await _reviewRepository.GetReviewByHotelIdAsync(id);
            var comments = await _commentRepository.GetCommentsByHotelIdAsync(id);

            // Truyền dữ liệu sang view
            ViewData["Review"] = review;
            ViewData["Comments"] = comments;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.FullName = user.FullName;

            return View(hotel);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(int hotelId, int service, int facilities, int cleanliness, int comfort, int location)
        {
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
    }
}
