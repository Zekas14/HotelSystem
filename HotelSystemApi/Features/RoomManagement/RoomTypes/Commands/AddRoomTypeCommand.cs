using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Models.Enums;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Commands
{
    public record AddRoomTypeCommand(string name, double price) : IRequest<ResponseViewModel<bool>>;

    public class AddRoomTypeCommandHandler : IRequestHandler<AddRoomTypeCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<RoomType> _repository;
        readonly IMediator _mediator;

        public AddRoomTypeCommandHandler(IRepository<RoomType> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResponseViewModel<bool>> Handle(AddRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);

            if (!response.IsSuccess)
                return response;

            _repository.Add(new RoomType
            {
                Name = request.name,
                Price = request.price,
            });

            return response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(AddRoomTypeCommand request)
        {
            if(string.IsNullOrEmpty(request.name))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "Name is required");
            }

            if (request.price <= 0)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Price must be greater than Zero");
            }

            var roomtypeExists = await _mediator.Send(new IsRoomTypeExistsQuery(request.name));

            if(roomtypeExists)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return new SuccessResponseViewModel<bool>(true);
        }
    }
}
