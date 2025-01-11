using HotelSystem.Data.Repository;
using HotelSystem.DTOs;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.Models.RoomManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Features.RoomManagement.Rooms.Queries
{
    public record GetAllRoomQuery() : IRequest<IEnumerable<RoomDTO>>;

    public class GetRoomTypesQueryHandler : IRequestHandler<GetAllRoomQuery, IEnumerable<RoomDTO>>
    {
        readonly IRepository<Room> _repository;
        public GetRoomTypesQueryHandler(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomDTO>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {
            var rooms = _repository.GetAll();
            if (rooms == null) return Enumerable.Empty<RoomDTO>();

            var RoomTypeViewModel = await rooms.ProjectTo<RoomDTO>().ToListAsync();

            return RoomTypeViewModel;
        }
    }
}
