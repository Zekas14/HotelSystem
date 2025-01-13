namespace HotelSystem.ViewModels.RoomManagment.Rooms
{
    public class UpdateRoomViewModel
    {

       public string RoomNumber { get; set; }
            
        public int RoomTypeID { get; set; }

        public List<int> FaciltesID { get; set; }
             
    }
}