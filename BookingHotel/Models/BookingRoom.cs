﻿namespace BookingHotel.Models
{
    public class BookingRoom
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public decimal Total { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
