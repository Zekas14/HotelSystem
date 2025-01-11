using HotelSystem.Features.RoomManagement.Facilities.Commands;
using HotelSystem.Features.RoomManagement.Facilities.Queries;
using HotelSystem.Features.RoomManagement.RoomTypes.Commands;
using HotelSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelSystem.Helper;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.RoomManagment.Facilities;
using HotelSystem.ViewModels.RoomManagment.RoomTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FaciltyController : ControllerBase
    {
        readonly IMediator _mediator;
        public FaciltyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ResponseViewModel<bool>> AddFacility(CreateFacilityViewModel viewModel)
        {
            var response = await _mediator.Send(new AddFaciltyCommand(viewModel.Name, viewModel.Price , viewModel.Description));

            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<FaclityViewModel>> GetAllFaclites()
        {
            var response = await _mediator.Send(new GetFaciltesQuery());

            var result = response.Select(c => new FaclityViewModel
            {
                Name = c.Name,
                Descrbtion  =c.Description, 
                Price = c.Price
            });       
           
            return result;
        }
        [HttpGet]
        public async Task<FaclityViewModel> Detials(int id)
        {
            var response = await _mediator.Send(new GetFaciltyDetilesQuery(id));

            var result= response.Data.MapTo<FaclityViewModel>();

            return result; 
        }

        [HttpPost("update")]
        public Task<ResponseViewModel<bool>> EDitFacilty(int id, EditFacilityViewModel viewModel)
        {
            var Response = _mediator.Send(new EditFacilityCommand(id, viewModel.Name, viewModel.Price));

            return Response;
        }

        [HttpPost("Delete")]

        public async Task<ResponseViewModel<bool>> DeleteFacilty(int id)
        {
            var response = await _mediator.Send(new DeleteFaciltyCommand(id));

            return response;
        }

    }
}
