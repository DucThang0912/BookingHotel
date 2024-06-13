using BookingHotel.Data;
using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context, IHotelRepository hotelRepository, IRegionRepository regionRepository, IServiceRepository serviceRepository, IHotelServiceRepository hotelServiceRepository)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _regionRepository = regionRepository;
            _serviceRepository = serviceRepository;
            _hotelServiceRepository = hotelServiceRepository;
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
        [HttpPost]
        public async Task<IActionResult> Add(Hotel hotel, IFormFile? imageUrl, IEnumerable<IFormFile>? additionalImages, int regionId)
        {
            //if (ModelState.IsValid)
            //{
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

                if (additionalImages != null)
                {
                    await SaveAdditionalImages(hotel.Id, additionalImages);
                }

                return RedirectToAction(nameof(Index));
            //}

            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            //var regions = await _regionRepository.GetAllAsync();
            //ViewBag.Regions = new SelectList(regions, "Id", "Name");
            //return View(hotel);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot/images/hotels", fileName); // Đường dẫn lưu trữ hình ảnh trên máy chủ

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/hotels/" + fileName; // Trả về đường dẫn tương đối của hình ảnh
        }
        private async Task SaveAdditionalImages(int hotelId, IEnumerable<IFormFile> additionalImages)
        {
            foreach (var image in additionalImages)
            {
                if (image.Length > 0)
                {
                    string imagePath = await SaveImage(image);
                    var hotelImage = new HotelImage
                    {
                        ImageUrl = imagePath,
                        HotelId = hotelId
                    };

                    await _hotelRepository.AddHotelImageAsync(hotelImage);
                }
            }
        }
        public async Task<IActionResult> Display(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            if (hotel.RegionId != null)
            {
                hotel.Region = await _regionRepository.GetByIdAsync(hotel.RegionId);
            }
            hotel.Images = await _hotelRepository.GetHotelImagesAsync(id);

            return View(hotel);
        }
        public async Task<IActionResult> Update(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            // Retrieve the list of HotelImage objects
            hotel.Images = await _hotelRepository.GetHotelImagesAsync(id);

            var regions = await _regionRepository.GetAllAsync();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return View(hotel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Hotel hotel, IFormFile? imageUrl, IEnumerable<IFormFile>? newImages)
        {
            var existingHotel = await _context.Hotels.Include(h => h.Images).FirstOrDefaultAsync(h => h.Id == hotel.Id);

            if (existingHotel == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính của existingHotel từ hotel
            existingHotel.Name = hotel.Name;
            existingHotel.Description = hotel.Description;
            existingHotel.Address = hotel.Address;
            existingHotel.RegionId = hotel.RegionId;

            if (imageUrl != null && imageUrl.Length > 0)
            {
                string imagePath = await SaveImage(imageUrl);
                existingHotel.ImageUrl = imagePath;
            }

            // Không xóa tất cả HotelImage hiện có
            // Thay vào đó, thêm các ảnh mới vào danh sách
            if (newImages != null)
            {
                await SaveAdditionalImages(existingHotel.Id, newImages);
            }

            _context.Hotels.Update(existingHotel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.HotelImages.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            _context.HotelImages.Remove(image);
            await _context.SaveChangesAsync();

            return Ok();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hotelRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddServicesToHotel(int id)
        {
            var services = await _serviceRepository.GetAllAsync();

            var selectedServices = await _hotelServiceRepository.GetServicesByHotelIdAsync(id);

            ViewBag.HotelId = id;

            if (selectedServices == null || !selectedServices.Any())
            {
                ViewBag.Services = services;
                return View(services);
            }

            ViewBag.SelectedServices = selectedServices;

            return View(services);
        }


        [HttpPost]
        public async Task<IActionResult> AddServiceToHotel(int selectedServiceId, int id)
        {
            if (selectedServiceId == 0)
            {
                return BadRequest("No service selected.");
            }

            var existingService = await _hotelServiceRepository.GetServiceByHotelIdAndServiceIdAsync(id, selectedServiceId);
            if (existingService != null)
            {
                // Nếu đã tồn tại, xóa dịch vụ
                await _hotelServiceRepository.DeleteAsync(id, selectedServiceId);
            }
            else
            {
                var hotelService = new HotelService
                {
                    HotelId = id,
                    ServiceId = selectedServiceId
                };

                await _hotelServiceRepository.AddAsync(hotelService);
            }
            return RedirectToAction("AddServicesToHotel", new { id = id });
        }
    }
}
