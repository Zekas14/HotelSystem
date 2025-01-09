using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels.RoomReservations;

namespace HotelSystem.ViewModels.GuestManagement
{
    public class GetGuestReservationsViewModel
    {
        public int ReservationId { get; set; } = 0;
        public int NumberOfGuests { get; internal set; }
        public string SpecialRequests { get; internal set; }
        public decimal TotalAmount { get; internal set; }
        public ReservationStatus ReservationStatus { get; internal set; }
        public IEnumerable<ReservationRoomViewModel> ReservationRooms { get; set; }
    }
    
}
