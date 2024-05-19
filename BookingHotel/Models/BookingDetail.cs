using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class BookingDetail
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? roomType { get; set; }
        public int Quantity { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Total { get; set; }
    }
}
