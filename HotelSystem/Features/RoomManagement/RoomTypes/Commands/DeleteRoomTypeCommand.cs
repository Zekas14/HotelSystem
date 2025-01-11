using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.RoomTypes.Commands
{
    public record DeleteRoomTypeCommand(int id) : IRequest<ResponseViewModel<bool>>;


    public class DeleteRoomTypeHandler : IRequestHandler<DeleteRoomTypeCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<RoomType> _repository;
        readonly IMediator _mediator; 

        public DeleteRoomTypeHandler(IMediator mediator, IRepository<RoomType> repository)
        {
            _repository = repository;
            _mediator = mediator;



        }
        public async Task<ResponseViewModel<bool>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            // vaildaition 
            var isExist = await _repository.AnyAsync(c => c.ID == request.id && !c.Deleted);
            if (!isExist) return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, " there is no room type with this id");

            _repository.Delete(new RoomType { ID = request.id });

            _repository.SaveChanges();

            return new SuccessResponseViewModel<bool>(true);

        }


    }
}


