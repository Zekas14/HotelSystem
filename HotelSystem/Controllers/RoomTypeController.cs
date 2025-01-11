using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomManagment.RoomTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomTypeController : ControllerBase
    {
        readonly IMediator _mediator;
        public RoomTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ResponseViewModel<bool>> Add(CreateRoomTypeViewModel viewModel)
        {
            var response = await _mediator.Send(new AddRoomTypeCommand(viewModel.Name, viewModel.Price));

            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomTypeViewModel>> GetAll()
        {
            var response = await _mediator.Send(new GetRoomTypesQuery());

            var result = response.Select(c => new RoomTypeViewModel
            {
               
                Name = c.Name,
                Price = c.Price,
            });       
           
            return result;
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
