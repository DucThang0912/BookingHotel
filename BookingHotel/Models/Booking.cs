using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]  
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
    public enum PaymentMethod
    {
        PayAtHotel,
        MoMo,
        CreditCard
    }
}

