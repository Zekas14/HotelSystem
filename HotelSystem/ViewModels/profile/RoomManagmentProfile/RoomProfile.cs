using AutoMapper;
using HotelSystem.DTOs;
using HotelSystem.Features.RoomManagement.Rooms.Command;
using HotelSystem.Models.RoomManagement;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HotelSystem.ViewModels.profile.RoomManagmentProfile
{
    public class RoomProfile : Profile
    {

       public RoomProfile() {
            CreateMap<AddRoomCommand, Room>().
                ForMember(dst=> dst.RoomFacilities,
                opt=> opt.MapFrom(c=>c.RoomFacilitiesID.Select(c=> new RoomFacility { FacilityID = c })));

            CreateMap<Room, RoomDTO>()
                .ForMember(dst => dst.Faciltes, opt => opt.MapFrom(s => s.RoomFacilities.Select(s => s.Facility.Name).ToList()))
                .ForMember(dst=> dst.Name , opt=> opt.MapFrom(s=> s.RoomType.Name))
                .ForMember(dst=> dst.Price , opt=> opt.MapFrom(s=>s.RoomType.Price + s.RoomFacilities.Sum(c=> c.Facility.Price))); 


        }

    }
}
