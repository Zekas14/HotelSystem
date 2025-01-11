using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.Facilities.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Facilities.Commands
{
    public record AddFaciltyCommand( string name, double price, string Description) : IRequest<ResponseViewModel<bool>>;


    public class AddFaciltyCommandHandler : IRequestHandler<AddFaciltyCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Facility> _repository;
        readonly IMediator _mediator;

        public AddFaciltyCommandHandler(IRepository<Facility> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResponseViewModel<bool>> Handle(AddFaciltyCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);

            if (!response.IsSuccess) return response;

            var NewFacilty = new Facility
            {
                Name = request.name,
                Price = request.price,
                Description = request.Description
            };

            _repository.Add(NewFacilty);

            _repository.SaveChanges();

            return response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(AddFaciltyCommand request)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "Name is required");
            }

            if (request.price <= 0)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Price must be greater than Zero");
            }

            var roomtypeExists = await _mediator.Send(new IsFacilityExistsQuery(request.name));

            if (roomtypeExists.IsSuccess)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return new SuccessResponseViewModel<bool>(true);


        }
    }

}
