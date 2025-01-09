using HotelSystem.Models.GuestMenagment;

namespace HotelSystem.Models.ReservationManagement
{
    public class Reservation
    {
        public ReservationStatus ReservationStatus { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int GuestID { get; set; }
        public Guest Guest { get; set; }
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
