﻿namespace BookingHotel.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}