﻿namespace BookingHotel.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int RegionId { get; set; }
        public Region? Region { get; set; }
        public string? ImageUrl { get; set; }
        public List<HotelImage>? Images { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
