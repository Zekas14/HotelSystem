
namespace HotelSystem.ViewModels.RoomReservations
{
    public class MakeReservationViewModel
    {
        public int RoomTypeId { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int GuestID { get; set; }
        public List<ReservationRoomViewModel> Reservations { get; set; }
    }
}
