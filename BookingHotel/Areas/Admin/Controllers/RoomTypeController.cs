using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeController(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var roomTypes = await _roomTypeRepository.GetAllAsync();
            return View(roomTypes);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _roomTypeRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                await _roomTypeRepository.AddAsync(roomType);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _roomTypeRepository.GetAllAsync();
            return View(roomType);
        }
        public async Task<IActionResult> Update(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _roomTypeRepository.UpdateAsync(roomType);
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomTypeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
