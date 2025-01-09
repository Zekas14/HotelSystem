using HotelSystem.Models.GuestMenagment;
using HotelSystem.Models.PaymentProcessing;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Models.ReservationManagement
{
    public class Reservation : BaseModel
    {
        public ReservationStatus ReservationStatus { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        [ForeignKey("Guest")]
        public int GuestID { get; set; }
        public Guest Guest { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
        [ForeignKey("PaymentTransaction")]
        public int PaymentTransactionID { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; } 
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
