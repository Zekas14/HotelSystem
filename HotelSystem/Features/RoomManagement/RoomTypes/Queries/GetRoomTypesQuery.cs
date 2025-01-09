using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Queries
{
    public record GetRoomTypesQuery() : IRequest<IEnumerable<RoomTypeDTO>>;

    public class GetRoomTypesQueryHandler : IRequestHandler<GetRoomTypesQuery, IEnumerable<RoomTypeDTO>>
    {
        readonly IRepository<RoomType> _repository;
        public GetRoomTypesQueryHandler(IRepository<RoomType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomTypeDTO>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .Select(x => new RoomTypeDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                }).ToListAsync();
        }
    }
}
