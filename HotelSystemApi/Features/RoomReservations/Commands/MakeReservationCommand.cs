using HotelSystem.Data.Repository;
using HotelSystem.Features.RoomReservations.Queries;
using HotelSystem.Models.Enums;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace HotelSystem.Features.RoomReservations.Commands
{
    public record MakeReservationCommand(MakeReservationViewModel viewModel) : IRequest<ResponseViewModel<bool>>
    {
    }
    public class MakeReservationHandler : IRequestHandler<MakeReservationCommand, ResponseViewModel<bool>>
    {

        IRepository<Reservation> _repository;
        IMediator _mediator;
        
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
                return new SuccessResponseViewModel<bool>(true);
            }
            return result;
        }
        public async Task<ResponseViewModel<bool>> Validate(MakeReservationCommand request)
        {

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
