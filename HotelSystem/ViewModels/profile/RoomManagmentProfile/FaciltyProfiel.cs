using AutoMapper;
using HotelSystem.Features.RoomManagement.Facilities.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels.RoomManagment.Facilities;

namespace HotelSystem.ViewModels.profile.RoomManagmentProfile
{
    public class FaciltyProfiel :Profile
    {

        public FaciltyProfiel() {

            CreateMap<Facility, FaciltyDTO>();
            CreateMap<FaciltyDTO, FaclityViewModel>();
        
        
        }
    }
}
