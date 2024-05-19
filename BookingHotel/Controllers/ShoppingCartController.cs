    using BookingHotel.Data;
    using BookingHotel.Helpers;
    using BookingHotel.Models;
    using BookingHotel.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace BookingHotel.Controllers
    {
        public class ShoppingCartController : Controller
        {
            private readonly IRoomRepository _roomRepository;
            private readonly IRoomTypeRepository _roomTypeRepository;
            private readonly ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            public ShoppingCartController(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository,ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            {
                _roomRepository = roomRepository;
                _roomTypeRepository = roomTypeRepository;
                _context = context;
                _userManager = userManager;

            }
            // Thêm các tham số mới vào phương thức AddToCartAsync
            //public async Task<IActionResult> AddToCartAsync(int roomId, DateTime checkInDate, DateTime checkOutDate, int adults, int children)
            //{
            //    Room room = await GetRoomFromDatabaseAsync(roomId);
            //    room.RoomType = await _roomTypeRepository.GetByIdAsync(roomId);
            //    if (room != null)
            //    {
            //        decimal nights = 0;
            //        decimal total = 0;
            //        if ((checkOutDate - checkInDate).Days == 0)
            //        {
            //            nights = 1;
            //            total = room.PricePerNight * nights;
            //        }
            //        else
            //        {
            //            nights = (checkOutDate - checkInDate).Days;
            //            total = room.PricePerNight * nights;
            //        }

            //        var cartItem = new CartItem
            //        {
            //            Id = roomId,
            //            Name = room.RoomType?.Description ?? "Unknown",
            //            Price = total,
            //            Quantity = 1,
            //            ImageUrl = room.Hotel?.ImageUrl ?? "No image",
            //            Adults = adults, // Lưu số người lớn
            //            Children = children, // Lưu số trẻ em
            //            CheckInDate = checkInDate, // Lưu ngày nhận phòng
            //            CheckOutDate = checkOutDate // Lưu ngày trả phòng
            //        };

            //        var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            //        cart.AddItem(cartItem);

            //        HttpContext.Session.SetObjectAsJson("Cart", cart);

            //        return RedirectToAction("Index");
            //    }
            //    return RedirectToAction("Index", "Room");
            //}


            public IActionResult Index()
            {
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
                return View(cart);
            }

            // Các actions khác...

            //private async Task<Room> GetRoomFromDatabaseAsync(int roomId)
            //{
            //    // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm

            //    return await _roomRepository.GetByIdAsync(roomId);
            //}


            //public IActionResult RemoveFromCart(int roomId)
            //{
            //    var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            //    cart.RemoveItem(roomId);
            //    HttpContext.Session.SetObjectAsJson("Cart", cart);
            //    return RedirectToAction("Index");
            //}

            public IActionResult Checkout()
            {
                return View(new Booking());
            }

            //[HttpPost]
            //public async Task<IActionResult> Checkout(Booking booking)
            //{
            //    var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            //    if (cart == null || !cart.Items.Any())
            //    {
            //        return RedirectToAction("Index");
            //    }

            //    var user = await _userManager.GetUserAsync(User);
            //    booking.UserId = user.Id;

            //    var bookingRooms = new List<BookingRoom>();
            //    foreach (var item in cart.Items)
            //    {
            //        var bookingRoom = new BookingRoom
            //        {
            //            RoomId = item.Id,
            //            Adults = item.Adults,
            //            Children = item.Children,
            //            CheckInDate = item.CheckInDate,
            //            CheckOutDate = item.CheckOutDate,
            //            Total = item.Price,
            //        };
            //        bookingRooms.Add(bookingRoom);
            //    }

            //    booking.BookingRooms = bookingRooms;
            //    _context.Bookings.Add(booking);
            //    await _context.SaveChangesAsync();

            //    HttpContext.Session.Remove("Cart");

            //    return RedirectToAction("BookingConfirmation", new { id = booking.Id });
            //}
            public IActionResult CheckoutNowForm(int roomId, DateTime checkInDate, DateTime checkOutDate, int totalGuests, string? ImageUrl)
            {
                ViewBag.RoomId = roomId;
                ViewBag.CheckInDate = checkInDate;
                ViewBag.CheckOutDate = checkOutDate;
                ViewBag.Adults = totalGuests;
                ViewBag.ImageUrl = ImageUrl;
                return View();
            }

            //[HttpPost]
            //public async Task<IActionResult> CheckoutNow(int roomId, DateTime checkInDate, DateTime checkOutDate, int adults, int children, Booking booking)
            //{
            //    var user = await _userManager.GetUserAsync(User);
            //    if (user == null)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    var room = await _roomRepository.GetByIdAsync(roomId);
            //    if (room == null)
            //    {
            //        return NotFound();
            //    }

            //    booking.UserId = user.Id;

            //    var bookingRoom = new BookingRoom
            //    {
            //        RoomId = roomId,
            //        Total = CalculateTotal(checkInDate, checkOutDate, room.PricePerNight),
            //        Adults = adults,
            //        Children = children,
            //        CheckInDate = checkInDate,
            //        CheckOutDate = checkOutDate
            //    };

            //    booking.BookingRooms = new List<BookingRoom> { bookingRoom };
            //    _context.Bookings.Add(booking);
            //    await _context.SaveChangesAsync();

            //    return RedirectToAction("BookingConfirmation", new { id = booking.Id });
            //}
            private decimal CalculateTotal(DateTime checkInDate, DateTime checkOutDate, decimal pricePerNight)
            {
                var nights = (checkOutDate - checkInDate).Days;
                return pricePerNight * nights;
            }
            //[HttpPost]
            //public IActionResult UpdateQuantity(int productId, int quantity)
            //{
            //    var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            //    cart.UpdateQuantity(productId, quantity);
            //    HttpContext.Session.SetObjectAsJson("Cart", cart);
            //    return RedirectToAction("Index");
            //}
            //public async Task<IActionResult> BookingConfirmation(int id)
            //{
            //    var booking = await _context.Bookings
            //        .Include(b => b.BookingRooms)
            //        .ThenInclude(br => br.Room)
            //        .ThenInclude(r => r.RoomType)
            //        .FirstOrDefaultAsync(b => b.Id == id);

            //    if (booking == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(booking);
            //}
        }
    }
