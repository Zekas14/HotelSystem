using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Features.RoomReservations.Commands;
using HotelSystem.Features.RoomReservations.Queries;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomManagment.RoomTypes;
using HotelSystem.ViewModels.RoomReservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservationController : ControllerBase
    {
        readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost] 
        public async Task<ResponseViewModel<bool>> MakeReservation(MakeReservationViewModel viewModel)
        {
            var response = await _mediator.Send(new MakeReservationCommand(viewModel));
            return response;
        }
        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<AvailableRoomViewModel>>> GetAvailableRooms(int? roomTypeID = null, DateTime? fromDate = null, DateTime? toDate = null, double? fromAmount = null, double? toAmount = null)
        {
            var result = await _mediator.Send(new GetAvailableRoomsQuery(roomTypeID, fromDate, toDate, fromAmount, toAmount));
            return new SuccessResponseViewModel<IEnumerable<AvailableRoomViewModel>>(result,"Successed") ;  
        }
        [HttpPut]
        public async Task<ResponseViewModel<bool>> CancelReservation(int reservationId, int guestId)
        {
            var response = await _mediator.Send(new CancelReservationCommand(reservationId, guestId));
            return response;
        }
    }
}
