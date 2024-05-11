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
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IRoomAmenityRepository _roomAmenityRepository;
        private readonly ApplicationDbContext _context;
        public RoomTypeController(ApplicationDbContext context, IRoomTypeRepository roomTypeRepository, IAmenityRepository amenityRepository, IRoomAmenityRepository roomAmenityRepository)
        {
            _context = context;
            _roomTypeRepository = roomTypeRepository;
            _amenityRepository = amenityRepository;
            _roomAmenityRepository = roomAmenityRepository;
        }   

        public async Task<IActionResult> Index(int hotelId)
        {
            var roomTypes = await _roomTypeRepository.GetRoomTypesByHotelIdAsync(hotelId);
            ViewBag.HotelId = hotelId;
            return View(roomTypes);
        }

        public async Task<IActionResult> Add(int hotelId)
        {
            ViewBag.HotelId = hotelId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoomType roomType, IFormFile? imageUrl, IEnumerable<IFormFile>? additionalImages, int hotelId)
        {
            if (imageUrl != null && imageUrl.Length > 0)
            {
                string imagePath = await SaveImage(imageUrl);
                roomType.ImageUrl = imagePath;
            }
            else
            {
                roomType.ImageUrl = null;
            }

            await _roomTypeRepository.AddAsync(roomType);

            if (additionalImages != null)
            {
                await SaveAdditionalImages(roomType.Id, additionalImages);
            }

            return RedirectToAction("Index", new { hotelId = hotelId });
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot/images/roomTypes", fileName); // Đường dẫn lưu trữ hình ảnh trên máy chủ

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/roomTypes/" + fileName; // Trả về đường dẫn tương đối của hình ảnh
        }
        private async Task SaveAdditionalImages(int roomTypeId, IEnumerable<IFormFile> additionalImages)
        {
            foreach (var image in additionalImages)
            {
                if (image.Length > 0)
                {
                    string imagePath = await SaveImage(image);
                    var roomTypeImage = new RoomTypeImage
                    {
                        ImageUrl = imagePath,
                        RoomTypeId = roomTypeId
                    };

                    await _roomTypeRepository.AddRoomTypeImageAsync(roomTypeImage);
                }
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            roomType.Images = await _roomTypeRepository.GetRoomTypeImagesAsync(id);
            return View(roomType);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoomType roomType, IFormFile? imageUrl, IEnumerable<IFormFile>? newImages)
        {
            var existingRoomType = await _context.RoomTypes
                .Include(rt => rt.Images)
                .FirstOrDefaultAsync(rt => rt.Id == roomType.Id);

            if (existingRoomType == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính của existingRoomType từ roomType
            existingRoomType.Name = roomType.Name;
            existingRoomType.TotalGuests = roomType.TotalGuests;
            existingRoomType.Description = roomType.Description;
            existingRoomType.PricePerNight = roomType.PricePerNight;

            if (imageUrl != null && imageUrl.Length > 0)
            {
                string imagePath = await SaveImage(imageUrl);
                existingRoomType.ImageUrl = imagePath;
            }

            // Không xóa tất cả RoomTypeImage hiện có
            // Thay vào đó, thêm các ảnh mới vào danh sách
            if (newImages != null)
            {
                await SaveAdditionalImages(existingRoomType.Id, newImages);
            }

            _context.RoomTypes.Update(existingRoomType);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { hotelId = existingRoomType.HotelId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.RoomTypeImages.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            _context.RoomTypeImages.Remove(image);
            await _context.SaveChangesAsync();

            return Ok();
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
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            await _roomTypeRepository.DeleteAsync(id);
            return RedirectToAction("Index", new { hotelId = roomType.HotelId });
        }

        public async Task<IActionResult> AddAmenitiesToRoomType(int id)
        {
            var amenities = await _amenityRepository.GetAllAsync();

            var selectedAmenities = await _roomAmenityRepository.GetAmenitiesByRoomIdAsync(id);

            ViewBag.RoomTypeId = id;

            if (selectedAmenities == null || !selectedAmenities.Any())
            {
                ViewBag.Amenities = amenities;
                return View(amenities);
            }

            ViewBag.SelectedAmenities = selectedAmenities;
            return View(amenities);
        }


        [HttpPost]
        public async Task<IActionResult> AddAmenityToRoomType(int selectedAmenityId, int id)
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
                    RoomTypeId = id,
                    AmenityId = selectedAmenityId
                };

                await _roomAmenityRepository.AddAsync(roomAmenity);
            }
            return RedirectToAction("AddAmenitiesToRoomType", new { id = id });
        }
    }
}
