namespace HotelSystem.Models.RoomManagement
{
    public class RoomFacility : BaseModel
    {
        public int RoomID { get; set; }
        public Room Room { get; set; }

        public int FacilityID { get; set; }
        public Facility Facility { get; set; }
    }
}
