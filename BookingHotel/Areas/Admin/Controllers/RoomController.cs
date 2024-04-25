using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IRoomAmenityRepository _roomAmenityRepository;

        public RoomController(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository, IRoomAmenityRepository roomAmenityRepository, IAmenityRepository amenityRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomAmenityRepository = roomAmenityRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<IActionResult> Index(int hotelId)
        {
            TempData["HotelId"] = hotelId; // Lưu hotelId vào TempData

            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            ViewBag.HotelId = hotelId;
            ViewBag.RoomTypes = new SelectList(await _roomTypeRepository.GetAllAsync(), "Id", "Description");

            return View(rooms);
        }

        public IActionResult Add(int hotelId)
        {
            ViewBag.HotelId = hotelId;
            ViewBag.RoomTypes = new SelectList(_roomTypeRepository.GetAllAsync().Result, "Id", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Room room, int hotelId)
        {
            room.HotelId = hotelId;
            await _roomRepository.AddAsync(room);
            return RedirectToAction("Index", new { hotelId = hotelId });
        }

        public async Task<IActionResult> Update(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            ViewBag.HotelId = room.HotelId;
            ViewBag.RoomTypes = new SelectList(await _roomTypeRepository.GetAllAsync(), "Id", "Description", room.RoomTypeId);
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Room room)
        {
            room.IsAvailable = false; // Set IsAvailable to false
            await _roomRepository.UpdateAsync(room);
            return RedirectToAction("Index", new { hotelId = room.HotelId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            await _roomRepository.DeleteAsync(id);
            return RedirectToAction("Index", new { hotelId = room.HotelId });
        }
        public async Task<IActionResult> AddAmenitiesToRoom(int id)
        {
            var amenities = await _amenityRepository.GetAllAsync();

            var selectedAmenities = await _roomAmenityRepository.GetAmenitiesByRoomIdAsync(id);

            ViewBag.RoomId = id;

            if (selectedAmenities == null || !selectedAmenities.Any())
            {
                ViewBag.Amenities = amenities;
                return View(amenities);
            }

            ViewBag.SelectedAmenities = selectedAmenities;
            return View(amenities);
        }


        [HttpPost]
        public async Task<IActionResult> AddAmenityToRoom(int selectedAmenityId, int id)
        {
            if (selectedAmenityId == 0)
            {
                return BadRequest("No amenity selected.");
            }

            var existingAmenity = await _roomAmenityRepository.GetAmenityByRoomIdAndAmenityIdAsync(id, selectedAmenityId);
            if (existingAmenity != null)
            {
                // Nếu đã tồn tại, xóa dịch vụ
                await _roomAmenityRepository.DeleteAsync(id, selectedAmenityId);
            }
            else
            {
                var roomAmenity = new RoomAmenity
                {
                    RoomId = id,
                    AmenityId = selectedAmenityId
                };

                await _roomAmenityRepository.AddAsync(roomAmenity);
            }
            return RedirectToAction("AddAmenitiesToRoom", new { id = id });
        }
    }
}
