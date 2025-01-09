using HotelSystem.Features.GuestManagement.Queries;
using HotelSystem.ViewModels.GuestManagement;
using HotelSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController(IMediator mediator): ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet(nameof(this.GetGuestReservations))]
        public async Task<ResponseViewModel<IQueryable<GetGuestReservationsViewModel>>> GetGuestReservations(int guestId)
        {
            var response = await mediator.Send(new GetGuestReservationsQuery(guestId));
            return response;
        }
    }
}
