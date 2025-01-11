using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Features.RoomManagement.Facilities.Queries
{
    public class FaciltyDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
    }
}