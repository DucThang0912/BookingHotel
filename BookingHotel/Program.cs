using Microsoft.EntityFrameworkCore;
using BookingHotel.Repositories;
using BookingHotel.Data;
using Microsoft.AspNetCore.Identity;
using BookingHotel.Models;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
})
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian hết hạn
    options.Cookie.HttpOnly = true; // Cookie chỉ được truy cập bằng HTTP
    options.Cookie.IsEssential = true; // Cookie là bắt buộc cho phiên
});

builder.Services.AddScoped<IHotelRepository, EFHotelRepository>();
builder.Services.AddScoped<IRoomRepository, EFRoomRepository>();
builder.Services.AddScoped<IRegionRepository, EFRegionRepository>();
builder.Services.AddScoped<IRoomTypeRepository, EFRoomTypeRepository>();
builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();
builder.Services.AddScoped<IAmenityRepository, EFAmenityRepository>();
builder.Services.AddScoped<IHotelServiceRepository, EFHotelServiceRepository>();
builder.Services.AddScoped<IRoomAmenityRepository, EFRoomAmenityRepository>();
builder.Services.AddScoped<IReviewRepository, EFReviewRepository>();
builder.Services.AddScoped<ICommentRepository, EFCommentRepository>();
builder.Services.AddScoped<IBookingRepository, EFBookingRepository>();
builder.Services.AddScoped<IBookingDetailRepository, EFBookingDetailRepository>();

// Thêm dịch vụ vào container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Đặt trước UseRouting
app.UseSession();

// Cấu hình pipeline xử lý yêu cầu HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Giá trị mặc định của HSTS là 30 ngày. Bạn có thể muốn thay đổi điều này cho các kịch bản sản xuất, xem https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Cấu hình Rotativa
var env = builder.Environment;
app.UseRotativa();
var contentRootPath = env.ContentRootPath; // Lấy đường dẫn thư mục gốc
var rotativaFolderPath = Path.Combine(contentRootPath, "wwwroot"); // Đường dẫn đến thư mục Rotativa
RotativaConfiguration.Setup(rotativaFolderPath);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Region}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Region}/{action=Index}/{id?}");

app.Run();
