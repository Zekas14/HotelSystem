using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Commands
{
    public record AddRoomTypeCommand(string Name, double Price) : IRequest<ResponseViewModel<bool>>;

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

            var roomtype = request.MapTo<RoomType>();   
            _repository.Add(roomtype);
            _repository.SaveChanges();
            return  response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(AddRoomTypeCommand request)
        {
            if(string.IsNullOrEmpty(request.Name))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty ,"yoy should but name ") ;
            }

            if (request.Price <= 0)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Price must be greater than 0");
            }

            var roomtypeExists = await _mediator.Send(new IsRoomTypeExistsQuery(request.Name));

            if (roomtypeExists)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return  new SuccessResponseViewModel<bool>(true);
        }
    }
}
