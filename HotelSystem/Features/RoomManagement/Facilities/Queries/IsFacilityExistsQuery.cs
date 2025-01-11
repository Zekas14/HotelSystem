using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Facilities.Queries
{
    public record IsFacilityExistsQuery(string name, int id = 0) : IRequest<ResponseViewModel<bool>>;

    public class IsFacilityExistsQueryHandler : IRequestHandler<IsFacilityExistsQuery, ResponseViewModel<bool>>
    {
        readonly IRepository<Facility> _repository;
        public IsFacilityExistsQueryHandler(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseViewModel<bool>> Handle(IsFacilityExistsQuery request, CancellationToken cancellationToken)
        {
            var exist = await _repository.AnyAsync(x => x.Name == request.name && (x.ID != request.id || x.ID == 0));
            if (exist) return new SuccessResponseViewModel<bool>(true);

            return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, "not found");
        }
    }
}
