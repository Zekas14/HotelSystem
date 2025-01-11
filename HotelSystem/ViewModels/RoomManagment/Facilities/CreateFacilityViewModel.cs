using System.ComponentModel.DataAnnotations;

namespace HotelSystem.ViewModels.RoomManagment.Facilities
{
    public class CreateFacilityViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
