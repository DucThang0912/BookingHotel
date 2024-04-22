using Microsoft.EntityFrameworkCore;
using BookingHotel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookingHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
    }
}

