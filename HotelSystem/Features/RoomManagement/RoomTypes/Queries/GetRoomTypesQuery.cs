using AutoMapper.QueryableExtensions;
using HotelSystem.Data.Repository;
using HotelSystem.DTOs;
using HotelSystem.Helper;

using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Queries
{
    public record GetRoomTypesQuery() : IRequest<IEnumerable<RoomTypeDTO>>;

    public class GetRoomTypesQueryHandler : IRequestHandler<GetRoomTypesQuery,IEnumerable<RoomTypeDTO>>
    {
        readonly IRepository<RoomType> _repository;
        public GetRoomTypesQueryHandler(IRepository<RoomType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomTypeDTO>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = _repository.GetAll();
            if (roomTypes == null) return Enumerable.Empty<RoomTypeDTO>(); 
           
            var RoomTypeViewModel =await   roomTypes.ProjectTo<RoomTypeDTO>().ToListAsync();

            return RoomTypeViewModel;
        }
    }
}
