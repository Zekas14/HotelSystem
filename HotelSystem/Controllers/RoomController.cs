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

            return response ;
        }

        [HttpPost("update")]
        public Task<ResponseViewModel<bool>> EDitRoomType(int id, UpdateRoomTypeViewModel viewModel)
        {
            var Response = _mediator.Send(new EditRoomTtypeCommand(id, viewModel.Name, viewModel.Price));

            return Response;
        }

        [HttpPost("Delete")]

        public async Task<ResponseViewModel<bool>> DeleteRoomType(int id)
        {
            var response = await _mediator.Send(new DeleteRoomTypeCommand(id));

            return response;
        }

    }
}
