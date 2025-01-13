using HotelSystem.DTOs;
using HotelSystem.Features.RoomManagement.Rooms.Command;
using HotelSystem.Features.RoomManagement.Rooms.Queries;
using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomManagment.Rooms;
using HotelSystem.ViewModels.RoomManagment.RoomTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomController : ControllerBase
    {
        readonly IMediator _mediator;
        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("avilableRooms")]
        public async Task<IEnumerable<RoomViewModel>> AvilableRoom(int? roomTypeID = null, double? fromAmount = null, double? toAmount = null) {
            var response = await _mediator.Send( new GetRoomsQuery(roomTypeID, fromAmount, toAmount)); 
         
            return response;
        }



        [HttpDelete("Delete")]

        public async Task<ResponseViewModel<bool>> DeleteRoom(int id)
        {
            var response = await _mediator.Send(new DeleteRoomCommand(id));

            return response;
        }




        [HttpPut("update")]
        public Task<ResponseViewModel<bool>> EDitRoom(int id, UpdateRoomViewModel viewModel)
        {
            var Response = _mediator.Send(new EditRoomCommand(id, viewModel.RoomNumber, viewModel.RoomTypeID , viewModel.FaciltesID ));

            return Response;
        }

        [HttpPost()]
        public async Task<ResponseViewModel<bool>> AddRoom(CreateRoomViewModel viewModel)
        {
            var response = await _mediator.Send(new AddRoomCommand
                (viewModel.RoomNumber, viewModel.Description ,viewModel.RoomTypeID , viewModel.FaciltiesID));

            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomDTO>> GetAll()
        {
            var response = await _mediator.Send(new GetAllRoomQuery());

            return response;
        }

  
     

    }
}
