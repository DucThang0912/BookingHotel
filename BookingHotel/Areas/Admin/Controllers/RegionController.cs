using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles = "Admin")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        public RegionController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var regions = await _regionRepository.GetAllAsync();
            return View(regions);
        }

        public async Task<IActionResult> Add()
        {
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Region region)
        {
            if (ModelState.IsValid)
            {
                await _regionRepository.AddAsync(region);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(region);
        }
        [HttpPost, ActionName("AddImage")]
        public async Task<IActionResult> Add(Region region, IFormFile? imageUrl, int regionId)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    region.ImageUrl = imagePath;
                }
                else
                {
                    region.ImageUrl = null;
                }
                await _regionRepository.AddAsync(region);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(region);
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
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(region);
        }
        [HttpPost, ActionName("UpdateImage")]
        public async Task<IActionResult> Update(Region region, IFormFile? imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string imagePath = await SaveImage(imageUrl);
                    region.ImageUrl = imagePath;
                }
                else
                {
                    region.ImageUrl = null;
                }
                await _regionRepository.UpdateAsync(region);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(region);
        }
    }
}
