using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Models.RoomManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomManagement.Facilities.Commands
{

    public record DeleteFaciltyCommand(int id) : IRequest<ResponseViewModel<bool>>;


    public class DeleteFaciltyCommandHandler : IRequestHandler<DeleteFaciltyCommand, ResponseViewModel<bool>>
    {
        readonly IRepository<Facility> _repository;


        public DeleteFaciltyCommandHandler(IMediator mediator, IRepository<Facility> repository)
        {
            _repository = repository;



        }
        public async Task<ResponseViewModel<bool>> Handle(DeleteFaciltyCommand request, CancellationToken cancellationToken)
        {

            var isExist = await _repository.AnyAsync(c => c.ID == request.id && !c.Deleted);
            if (!isExist) return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, " there is no facilty with this id");

            _repository.Delete(new Facility { ID = request.id });

            _repository.SaveChanges();

            return new SuccessResponseViewModel<bool>(isExist);

        }
    }
}

