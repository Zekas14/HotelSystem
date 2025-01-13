using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace HotelSystem.Features.RoomManagement.Rooms.Queries
{
    public record IsRoomNumberExistsQuery(string roomNumber) : IRequest<bool>;
    public class IsRoomNumberExistsQueryHandler : IRequestHandler<IsRoomNumberExistsQuery, bool>

    {
        IRepository<Room> _repository;

        public IsRoomNumberExistsQueryHandler(IRepository<Room> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(IsRoomNumberExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _repository.AnyAsync(c => c.RoomNumber == request.roomNumber);

            return exists;
        }
    }

}