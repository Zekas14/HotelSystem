using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.GuestManagement.Queries;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.RoomReservations.Commands
{
    public record CancelReservationCommand(int reservationId , int guestId) : IRequest<ResponseViewModel<bool>>
    {
    }
    public class CancelReservationCommandHandler(IRepository<Reservation> repository, IMediator mediator) : IRequestHandler<CancelReservationCommand, ResponseViewModel<bool>>
    {
        private readonly IRepository<Reservation> repository = repository;
        private readonly IMediator mediator = mediator;

        public async Task<ResponseViewModel<bool>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetGuestReservationsQuery(request.guestId));
            var reservation = data.Data.FirstOrDefault(r => r.ReservationId == request.reservationId);
            if (reservation == null)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.ItemNotFound, "Reservation not found");
            }
            if (reservation.ReservationStatus == ReservationStatus.Canceled)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Reservation already cancelled");
            }
            reservation.ReservationStatus = ReservationStatus.Canceled;
            repository.SaveChanges();
            return new SuccessResponseViewModel<bool>(true);

        }
    }
}
