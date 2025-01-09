using HotelSystem.Models.GuestMenagment;
using HotelSystem.Models.ReservationManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Models.PaymentProcessing
{
    public class Invoice:BaseModel
    {
        [ForeignKey("Guest")]
        public int GuestId { get;set; }
        public Guest Guest { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<PaymentTransaction> transactions { get; set; }
    }
}
