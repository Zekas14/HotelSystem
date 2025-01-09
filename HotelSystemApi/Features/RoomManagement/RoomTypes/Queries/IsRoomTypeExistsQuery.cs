using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using MediatR;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Queries
{
    public record IsRoomTypeExistsQuery(string name) : IRequest<bool>;

    public class IsRoomTypeExistsQueryHandler : IRequestHandler<IsRoomTypeExistsQuery, bool>
    {
        readonly IRepository<RoomType> _repository;
        public IsRoomTypeExistsQueryHandler(IRepository<RoomType> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(IsRoomTypeExistsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AnyAsync(x => x.Name == request.name);
        }
    }
}
