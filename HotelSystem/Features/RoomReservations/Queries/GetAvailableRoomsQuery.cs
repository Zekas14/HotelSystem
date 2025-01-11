using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomManagement.Rooms.Queries;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;

namespace HotelSystem.Features.RoomReservations.Queries
{
    public record GetAvailableRoomsQuery(int? roomTypeID = null, DateTime? fromDate = null, DateTime? toDate = null, double? fromAmount = null, double? toAmount = null) : IRequest<IEnumerable<AvailableRoomViewModel>>;

    public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, IEnumerable<AvailableRoomViewModel>>
    {
        IRepository<ReservationRoom> _repository;
        IMediator _mediator;

        public GetAvailableRoomsQueryHandler(IRepository<ReservationRoom> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AvailableRoomViewModel>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _mediator.Send(new GetRoomsQuery(request.roomTypeID, request.fromAmount, request.toAmount));
            var availableRooms = rooms.Select(r=>new AvailableRoomViewModel()
            {
                RoomID = r.ID,
                BasicPrice = r.BasicPrice,
                RoomType= r.RoomTypeName,
            });
            return availableRooms;
            // Get All Rooms that have NO reservation in the date range
        }
    }
}
