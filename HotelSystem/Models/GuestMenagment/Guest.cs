namespace HotelSystem.Models.GuestMenagment
{
    public class Guest : BaseModel
    {
        public string Name { get; set; }
        public string NID { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
    }
}
