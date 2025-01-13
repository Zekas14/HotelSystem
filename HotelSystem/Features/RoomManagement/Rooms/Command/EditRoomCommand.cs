using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.Rooms.Queries;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Rooms.Command
{
    public record EditRoomCommand(int id, string RoomNumber,int RoomTypeID , List<int> FaciltesID) : IRequest<ResponseViewModel<bool>>;


    public class EditRoomCommandHandler : IRequestHandler<EditRoomCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Room> _repository;
        readonly IMediator _mediator;

        public EditRoomCommandHandler(IMediator mediator, IRepository<Room> repository)
        {
            _repository = repository;

            _mediator = mediator;

        }

        public async Task<ResponseViewModel<bool>> Handle(EditRoomCommand request, CancellationToken cancellationToken)
        {

            var valdiation = await valildateRequest(request);
            if (!valdiation.IsSuccess) return valdiation;

            var EditingRoom = new Room {
                RoomNumber = request.RoomNumber , 
                RoomTypeID =request.RoomTypeID , 
                RoomFacilities = request.FaciltesID.
                Select(c=> new RoomFacility {  FacilityID = c }).ToList()}; 

            _repository.SaveInclude(EditingRoom, nameof(EditingRoom.RoomNumber), nameof(EditingRoom.RoomTypeID),nameof(EditingRoom.RoomFacilities));
            _repository.SaveChanges();
            return valdiation;
        }

        private async Task<ResponseViewModel<bool>> valildateRequest(EditRoomCommand editRoom)
        {

            if (string.IsNullOrEmpty(editRoom.RoomNumber)) return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "name is requird ");

            if (editRoom.RoomTypeID> 0 ) return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, " id must be greater than 0 ");

            var isExist = await _mediator.Send(new IsRoomNumberExistsQuery(editRoom.RoomNumber));

            if (isExist) return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, "there no item with this name ");

            return new SuccessResponseViewModel<bool>(true);
        }
    }
}
