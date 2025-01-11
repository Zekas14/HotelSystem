using HotelSystem.Features.RoomManagement.RoomTypes.Queries;

namespace HotelSystem.DTOs
{
    public class RoomDTO
    {
        public string RoomNumber { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }

        public List<string> Faciltes { get; set; }
    }


}