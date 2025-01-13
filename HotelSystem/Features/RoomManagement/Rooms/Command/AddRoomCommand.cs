using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.Rooms.Queries;
using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Rooms.Command
{
    public record AddRoomCommand (string RoomNumber  , string Description,int RoomTypeID , List<int> RoomFacilitiesID) :IRequest<ResponseViewModel<bool>>;


    public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Room> _repository;
        readonly IMediator _mediator;

        public AddRoomCommandHandler(IRepository<Room> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResponseViewModel<bool>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);

            if (!response.IsSuccess)
                return response;

            var room=request.MapTo<Room>();
            _repository.Add(room);
            _repository.SaveChanges();
            return response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(AddRoomCommand request)
        {
            if (string.IsNullOrEmpty(request.RoomNumber))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "yoy should but name ");
            }

        

            var RoomNumberExist = await _mediator.Send(new IsRoomNumberExistsQuery(request.RoomNumber));

            if (RoomNumberExist)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return new SuccessResponseViewModel<bool>(true);
        }
    }
}

