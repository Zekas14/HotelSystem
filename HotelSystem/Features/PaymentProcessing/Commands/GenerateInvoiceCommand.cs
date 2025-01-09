using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.PaymentProcessing.Commands
{
    public record GenerateInvoiceCommand():IRequest<ResponseViewModel<bool>>;
}
