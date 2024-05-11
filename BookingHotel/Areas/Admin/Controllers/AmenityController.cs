using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AmenityController : Controller
    {
        private readonly IAmenityRepository _amenityRepository;
        public AmenityController(IAmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }
        public async Task<IActionResult> Index()
        {
            var amenities = await _amenityRepository.GetAllAsync();
            return View(amenities);
        }

        public async Task<IActionResult> Add()
        {
            var amenities = await _amenityRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                await _amenityRepository.AddAsync(amenity);
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }
        public async Task<IActionResult> Update(int id)
        {
            var roomType = await _amenityRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _amenityRepository.UpdateAsync(amenity);
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var amenity = await _amenityRepository.GetByIdAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }
            return View(amenity);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _amenityRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
