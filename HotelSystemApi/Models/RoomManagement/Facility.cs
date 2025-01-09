namespace HotelSystem.Models.RoomManagement
{
    public class Facility : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();
    }
}
