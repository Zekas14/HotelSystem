using AutoMapper;
using HotelSystem.DTOs;
using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels.RoomManagment.RoomTypes;

namespace HotelSystem.ViewModels.profile.RoomManagmentProfile
{
    public class RoomTypeProfile : Profile
    {

        public RoomTypeProfile() {
            CreateMap<AddRoomTypeCommand, RoomType>();
            CreateMap<RoomType, RoomTypeDTO>(); 
            CreateMap<RoomTypeDTO, RoomTypeViewModel>();

        }


    }
}
