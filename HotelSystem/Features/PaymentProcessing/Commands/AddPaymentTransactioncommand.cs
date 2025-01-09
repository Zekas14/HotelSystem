using HotelSystem.Data.Repository;
using HotelSystem.Models.Enums;
using HotelSystem.Models.PaymentProcessing;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.PaymentProcessing.Commands
{
    public record AddPaymentTransactionCommand(string PaymentMethodNonce, decimal AmountPaid, int ReservationId) 
        :IRequest<ResponseViewModel<bool>>;
    public class AddPaymentTransactionCommandHandler(IRepository<PaymentTransaction> repository) : IRequestHandler<AddPaymentTransactionCommand, ResponseViewModel<bool>>
    {
        private readonly IRepository<PaymentTransaction> repository = repository;

        public async Task<ResponseViewModel<bool>> Handle(AddPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            var response = await Validate(request);
            if (!response.IsSuccess)
            {
                return response;
            }
            repository.Add(new PaymentTransaction
            {
                PaymentMethodNonce=request.PaymentMethodNonce,
                AmountPaid = request.AmountPaid,
                ReservationId = request.ReservationId

            });
            repository.SaveChanges();
            return new SuccessResponseViewModel<bool>(true, "Payment Transaction Added Successfully");
        }
        private async Task<ResponseViewModel<bool>> Validate(AddPaymentTransactionCommand request)
        {
            if (request.AmountPaid < 0)
            {
                return new FaluireResponseViewModel<bool>(ErrorCode.InvalidAmount, "Amount must be greater than 0");
            }
            return new SuccessResponseViewModel<bool>(true);
        }
    }
}
