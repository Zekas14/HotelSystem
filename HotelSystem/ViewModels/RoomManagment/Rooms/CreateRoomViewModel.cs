namespace HotelSystem.ViewModels.RoomManagment.Rooms
{
    public class CreateRoomViewModel
    {
        public string RoomNumber { get; set; }

        public string Description { get; set; } 

       public int RoomTypeID { get; set; }

        public List<int>FaciltiesID { get; set; }
    }
}
