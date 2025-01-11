using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomReservations.Queries;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace HotelSystem.Features.RoomReservations.Commands
{
    public record MakeReservationCommand(MakeReservationViewModel viewModel) : IRequest<ResponseViewModel<bool>>;
    public class MakeReservationHandler(IMediator mediator, IRepository<Reservation> repository) : IRequestHandler<MakeReservationCommand, ResponseViewModel<bool>>
    {

        IRepository<Reservation> _repository = repository;
        IMediator _mediator  = mediator;
        
        public async Task<ResponseViewModel<bool>> Handle(MakeReservationCommand request, CancellationToken cancellationToken)
        {
            var result = await Validate(request);
            
            if (result.IsSuccess)
            {
                Reservation reservation = new()
                {
                    GuestID = request.viewModel.GuestID,
                    NumberOfGuests = request.viewModel.NumberOfGuests,
                    SpecialRequests = request.viewModel.SpecialRequests,
                    TotalAmount = request.viewModel.TotalAmount,
                    ReservationStatus = ReservationStatus.Pending,
                };
                reservation.ReservationRooms = request.viewModel.Reservations.Select(r => new ReservationRoom
                {
                    RoomID = r.RoomID,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    Amount =r.Amount
                }).ToList();
                _repository.Add(reservation);
                _repository.SaveChanges();
                return new SuccessResponseViewModel<bool>(true);
            }
            return result;
        }
        public async Task<ResponseViewModel<bool>> Validate(MakeReservationCommand request)
        {
            var availableRooms = await _mediator.Send(new GetAvailableRoomsQuery(request.viewModel.RoomTypeId));
            if (!availableRooms.IsNullOrEmpty())
            {
                var availableRoomIds = availableRooms.Select(r => r.RoomID);
                if (request.viewModel.Reservations.Any(r => !availableRoomIds.Contains(r.RoomID)))
                {
                    return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Room is not available");
                }
            }
            else
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "Room Type not found");
            }

            if (request.viewModel.Reservations.Any(r=>r.CheckInDate == null) || request.viewModel.Reservations.Any(r=>r.CheckOutDate == null))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "CheckInDate and CheckOutDate are required");
            }
            if (request.viewModel.Reservations.Any(r=>r.CheckInDate  < DateTime.Now))
            {
                return  new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "CheckInDate must be greater than current date");
            }
            if (request.viewModel.Reservations.Any(r => r.CheckOutDate < r.CheckInDate))
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidInput, "CheckOutDate must be greater than CheckInDate");
            }
            return new SuccessResponseViewModel<bool>(true);
        }   
    }
}
