using HotelSystem.Data.Enums;
using HotelSystem.Models.GuestMenagment;
using HotelSystem.Models.ReservationManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Models.PaymentProcessing
{
    public class PaymentTransaction: BaseModel
    {
        public decimal AmountPaid { get; set; }
        public PaymentStatus PaymentStatus { get
            {
                return AmountPaid switch
                {
                    0 when AmountPaid < Reservation.TotalAmount => PaymentStatus.NotPaid,
                    > 0 when AmountPaid < Reservation.TotalAmount => PaymentStatus.PartiallyPaid,
                    >= 0 when AmountPaid == Reservation.TotalAmount => PaymentStatus.Paid,
                    _ => PaymentStatus.NotPaid,
                };
            }
        } 
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; } 
        public string PaymentMethodNonce { get; set; }    
        public Reservation Reservation { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
    }
}
