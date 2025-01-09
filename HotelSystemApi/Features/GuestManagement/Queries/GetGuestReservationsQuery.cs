using HotelSystem.Data.Repository;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.GuestManagement;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;

namespace HotelSystem.Features.GuestManagement.Queries
{
    public record GetGuestReservationsQuery(int guestId) : IRequest<ResponseViewModel<IQueryable<GetGuestReservationsViewModel>>>
    {

    }
    public class GetGuestReservationQueryHandler(IMediator mediator,IRepository<Reservation> repository) : IRequestHandler<GetGuestReservationsQuery, ResponseViewModel<IQueryable<GetGuestReservationsViewModel\>
    {
        private readonly IMediator mediator = mediator;
        private readonly IRepository<Reservation> repository = repository;

        public async Task<ResponseViewModel<IQueryable< GetGuestReservationsViewModel>>> Handle(GetGuestReservationsQuery request, CancellationToken cancellationToken)
        {
              var reservations = repository.Get(r => r.GuestID == request.guestId);
            var data =
                 reservations.Select(r => new GetGuestReservationsViewModel
                 {
                     NumberOfGuests = r.NumberOfGuests,
                     SpecialRequests = r.SpecialRequests,
                     TotalAmount = r.TotalAmount,
                     ReservationStatus = r.ReservationStatus,
                     ReservationRooms = r.ReservationRooms.Select(rr => new ReservationRoomViewModel()
                     {
                         RoomID = rr.RoomID,
                         CheckInDate = rr.CheckInDate,
                         CheckOutDate = rr.CheckOutDate,
                         Amount = rr.Amount,
                     })
                 });
            return new SuccessResponseViewModel<IQueryable<GetGuestReservationsViewModel>>(data);
        }
        }
    }

