using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Add(Amenity amenity, IFormFile? imageUrl, int regionId)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    amenity.ImageUrl = imagePath;
                }
                else
                {
                    amenity.ImageUrl = null;
                }
                await _amenityRepository.AddAsync(amenity);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            return View(amenity);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot/images/amenities", fileName); // Đường dẫn lưu trữ hình ảnh trên máy chủ

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/amenities/" + fileName; // Trả về đường dẫn tương đối của hình ảnh
        }
        public async Task<IActionResult> Update(int id)
        {
            var amenity = await _amenityRepository.GetByIdAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }
            return View(amenity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Amenity amenity, IFormFile? imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    amenity.ImageUrl = imagePath;
                }
                else
                {
                    amenity.ImageUrl = null;
                }
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
