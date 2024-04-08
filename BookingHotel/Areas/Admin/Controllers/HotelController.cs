using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRegionRepository _regionRepository;
        
        public HotelController(IHotelRepository hotelRepository, IRegionRepository regionRepository)
        {
            _hotelRepository = hotelRepository;
            _regionRepository = regionRepository;
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
        public async Task<IActionResult> Add()
        {
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotelRepository.AddAsync(hotel);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(hotel);
        }

        [HttpPost, ActionName("AddImage")]
        public async Task<IActionResult> Add(Hotel hotel, IFormFile? imageUrl, int regionId)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    hotel.ImageUrl = imagePath;
                }
                else
                {
                    hotel.ImageUrl = null;
                }
                await _hotelRepository.AddAsync(hotel);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(hotel);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName); // Đường dẫn lưu trữ hình ảnh trên máy chủ

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + fileName; // Trả về đường dẫn tương đối của hình ảnh
        }
        public async Task<IActionResult> Update(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(hotel);
        }
        [HttpPost, ActionName("UpdateImage")]
        public async Task<IActionResult> Update(Hotel hotel, IFormFile? imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    hotel.ImageUrl = imagePath;
                }
                else
                {
                    hotel.ImageUrl = null;
                }
                await _hotelRepository.UpdateAsync(hotel);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(hotel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var region = await _hotelRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hotelRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
