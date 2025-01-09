
namespace HotelSystem.ViewModels.RoomReservations
{
    public class ReservationRoomViewModel
    {
        public int RoomID { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Amount { get; set; }
    }
}
