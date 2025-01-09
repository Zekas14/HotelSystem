namespace HotelSystem.Models.RoomManagement
{
    public class Room : BaseModel
    {
        public string RoomNumber { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();
    }
}
