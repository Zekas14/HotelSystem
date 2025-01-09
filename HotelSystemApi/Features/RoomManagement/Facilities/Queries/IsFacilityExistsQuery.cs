using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Facilitys.Queries
{
    public record IsFacilityExistsQuery(string name,int id=0) : IRequest<bool>;

    public class IsFacilityExistsQueryHandler : IRequestHandler<IsFacilityExistsQuery, bool>
    {
        readonly IRepository<Facility> _repository;
        public IsFacilityExistsQueryHandler(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(IsFacilityExistsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AnyAsync(x => x.Name == request.name && (x.ID !=request.id || x.ID ==0));
        }
    }
}
