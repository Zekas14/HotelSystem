using Braintree;
using HotelSystem.Data.Enums;
using HotelSystem.Data.Repository;
using HotelSystem.Features.PaymentProcessing.Commands;
using HotelSystem.Models.Enums;
using HotelSystem.Models.PaymentProcessing;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.Payment_Processing.Commands
{
    public record ProcessPaymentCommand(string PaymentMethodNonce, decimal Amount , int ReservationId) : IRequest<ResponseViewModel<bool>>;

    public class ProcessPaymentCommandHandler(IMediator mediator, IBraintreeGateway braintreeGateway)
        : IRequestHandler<ProcessPaymentCommand, ResponseViewModel<bool>>
    {
        private readonly IMediator mediator = mediator;
        private readonly IBraintreeGateway braintreeGateway = braintreeGateway;
        
        public async Task<ResponseViewModel<bool>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var transactionRequest = new TransactionRequest()
            {
                Amount = request.Amount,
                PaymentMethodNonce = request.PaymentMethodNonce,
                Options = new TransactionOptionsRequest()
                {
                    SubmitForSettlement = true
                }
            };
            var result = await braintreeGateway.Transaction.SaleAsync(transactionRequest);
            if (result.IsSuccess())
            {
                // Add payment transaction to the database
               var response =  await mediator.Send(new AddPaymentTransactionCommand(request.PaymentMethodNonce, request.Amount, request.ReservationId));
                if (response.IsSuccess)
                {
                    return new SuccessResponseViewModel<bool>(true, "Transaction Successed");
                }
                else
                {
                    return response;
                }
            }
            return new FaluireResponseViewModel<bool>(ErrorCode.UnKnownError,result.Message);
        }
    }
}
