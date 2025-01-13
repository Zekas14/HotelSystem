using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Rooms.Command
{
    public record DeleteRoomCommand(int id) : IRequest<ResponseViewModel<bool>>;


    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Room> _repository;
        readonly IMediator _mediator;

        public DeleteRoomHandler(IMediator mediator, IRepository<Room> repository)
        {
            _repository = repository;
            _mediator = mediator;



        }
        public async Task<ResponseViewModel<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            // vaildaition 
            var isExist = await _repository.AnyAsync(c => c.ID == request.id && !c.Deleted);
            if (!isExist) return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, " there is no room type with this id");

            _repository.Delete(new Room { ID = request.id });

            _repository.SaveChanges();

            return new SuccessResponseViewModel<bool>(true);

        }
    }
}
