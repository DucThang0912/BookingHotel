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
        private readonly IServiceRepository _serviceRepository;
        private readonly IHotelServiceRepository _hotelServiceRepository;
        
        public HotelController(IHotelRepository hotelRepository, IRegionRepository regionRepository, IServiceRepository serviceRepository, IHotelServiceRepository hotelServiceRepository)
        {
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
            var filePath = Path.Combine("wwwroot/images/hotels", fileName); // Đường dẫn lưu trữ hình ảnh trên máy chủ

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/hotels/" + fileName; // Trả về đường dẫn tương đối của hình ảnh
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
        [HttpPost]
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
            // Lấy danh sách dịch vụ từ repository
            var services = await _serviceRepository.GetAllAsync();

            // Lấy danh sách dịch vụ đã được chọn của khách sạn
            var selectedServices = await _hotelServiceRepository.GetServicesByHotelIdAsync(id);

            // Truyền giá trị HotelId vào ViewBag
            ViewBag.HotelId = id;

            // Nếu không có dữ liệu trong danh sách đã chọn, chỉ hiển thị danh sách dịch vụ
            if (selectedServices == null || !selectedServices.Any())
            {
                ViewBag.Services = services;
                return View(services);
            }

            // Nếu có dữ liệu, truyền cả danh sách đã chọn vào view
            ViewBag.SelectedServices = selectedServices;

            return View(services);
        }


        [HttpPost]
        public async Task<IActionResult> AddServiceToHotel(int selectedServiceId, int id)
        {
            if (selectedServiceId == 0)
            {
                // Trả về một thông báo lỗi nếu không có dịch vụ nào được chọn
                return BadRequest("No service selected.");
            }

            // Kiểm tra xem dịch vụ đã tồn tại cho khách sạn hay chưa
            var existingService = await _hotelServiceRepository.GetServiceByHotelIdAndServiceIdAsync(id, selectedServiceId);
            if (existingService != null)
            {
                // Nếu đã tồn tại, xóa dịch vụ
                await _hotelServiceRepository.DeleteAsync(id, selectedServiceId);
            }
            else
            {
                // Nếu chưa tồn tại, thêm dịch vụ mới
                var hotelService = new HotelService
                {
                    HotelId = id,
                    ServiceId = selectedServiceId
                };

                await _hotelServiceRepository.AddAsync(hotelService);
            }

            // Chuyển hướng người dùng về trang AddServicesToHotel sau khi thực hiện thành công
            return RedirectToAction("AddServicesToHotel", new { id = id });
        }


    }
}
