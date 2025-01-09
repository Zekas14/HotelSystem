using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels.RoomManagment.Facilities;

namespace HotelSystem.ViewModels.RoomReservations
{
    public class AvailableRoomViewModel
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }

        public double BasicPrice { get; set; }

        public IEnumerable<FaclityViewModel> Faclities { get; set; }
    }
}
