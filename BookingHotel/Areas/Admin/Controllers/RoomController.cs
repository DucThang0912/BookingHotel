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

        public RoomController(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<IActionResult> Index(int roomTypeId)
        {
            var rooms = await _roomRepository.GetRoomsByRoomTypeIdAsync(roomTypeId);
            ViewBag.RoomTypeId = roomTypeId;
            return View(rooms);
        }
        public async Task<IActionResult> Add(int roomTypeId)
        {
            ViewBag.RoomTypeId = roomTypeId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomRepository.AddAsync(room);
                return RedirectToAction("Index", new { roomTypeId = room.RoomTypeId });
            }
            return RedirectToAction("Index", new { roomTypeId = room.RoomTypeId });
        }
        public async Task<IActionResult> Update(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _roomRepository.UpdateAsync(room);
                return RedirectToAction("Index", new { roomTypeId = room.RoomTypeId });
            }
            return View(room);
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
            await _roomRepository.DeleteAsync(id);
            return RedirectToAction("Index", new { roomTypeId = room.RoomTypeId });
        }
    }
}
