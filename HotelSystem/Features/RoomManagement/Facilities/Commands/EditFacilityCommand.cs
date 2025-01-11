

using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.Facilities.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Facilities.Commands
{
    public record EditFacilityCommand(int id, string name, double price) : IRequest<ResponseViewModel<bool>>;

    public class EditFacilityCommandHandler : IRequestHandler<EditFacilityCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Facility> _repository;
        readonly IMediator _mediator;

        public EditFacilityCommandHandler(IRepository<Facility> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResponseViewModel<bool>> Handle(EditFacilityCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);

            if (!response.IsSuccess)
                return response;
            var facility = new Facility { ID = request.id, Name = request.name, Price = request.price };
            _repository.SaveInclude(facility, nameof(facility.Name), nameof(facility.Price));
            _repository.SaveChanges();
            return response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(EditFacilityCommand request)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "Name is required");
            }

            if (request.price <= 0)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Price must be greater than Zero");
            }

            var facilityExists = await _mediator.Send(new IsFacilityExistsQuery(request.name, request.id));

            if (!facilityExists.IsSuccess)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return new SuccessResponseViewModel<bool>(true);

        }
    }
}
