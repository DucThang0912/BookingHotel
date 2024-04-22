using Microsoft.EntityFrameworkCore;
using BookingHotel.Repositories;
using BookingHotel.Data;
using Microsoft.AspNetCore.Identity;
using BookingHotel.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

})
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IHotelRepository, EFHotelRepository>();
builder.Services.AddScoped<IRoomRepository, EFRoomRepository>();
builder.Services.AddScoped<IRegionRepository, EFRegionRepository>();
builder.Services.AddScoped<IRoomTypeRepository, EFRoomTypeRepository>();
builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();
builder.Services.AddScoped<IAmenityRepository, EFAmenityRepository>();
builder.Services.AddScoped<IHotelServiceRepository, EFHotelServiceRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//---------------------
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

//---------------------
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Region}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "admin",
//    pattern: "{area:exists}/{controller=Admin}/{action=Index}"
//);
//app.MapControllerRoute(
//          name: "admin",
//          pattern: "{area:exists}/{controller=Region}/{action=Index}/{id?}"
//);
//-------------------------
app.Run();
