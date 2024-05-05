using Microsoft.AspNetCore.Mvc;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookingHotel.Models;
using System.Drawing;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IRegionRepository regionRepository, IRoomTypeRepository roomTypeRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _regionRepository = regionRepository;
            _roomTypeRepository = roomTypeRepository;
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

            // Lấy danh sách phòng và bao gồm thông tin RoomType của mỗi phòng
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(id, includeRoomType: true);

            hotel.Images = hotelImages;
            hotel.Rooms = rooms.ToList();

            // Truyền dữ liệu hotel sang view
            return View(hotel);
        }
    }
}
