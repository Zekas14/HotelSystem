using HotelSystem.Features.Payment_Processing.Commands;
using HotelSystem.ViewModels;
using HotelSystem.ViewModels.PaymentProcessing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        [Route("ProcessPayment")]
        public async Task<ResponseViewModel<bool>> ProcessPayment([FromBody] ProcessPaymentViewModel model)
        {
            var response =await mediator.Send(new ProcessPaymentCommand(model.PaymentMethodNonce, model.Amount, model.ReservationId));
            return response;
        }
    }
}
