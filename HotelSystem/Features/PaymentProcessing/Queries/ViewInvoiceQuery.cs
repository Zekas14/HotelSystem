using HotelSystem.Data.Repository;
using HotelSystem.Models.PaymentProcessing;
using HotelSystem.ViewModels;
using MediatR;

namespace HotelSystem.Features.PaymentProcessing.Queries
{
    public record ViewInvoiceQuery
        (int InvoiceId, int ReservationId, string GuestName, string RoomType, double AmountPaid,
        double TotalAmount, double Remain, string PaymentMethod, DateTime DateIssued, string PaymentStatus)
        : IRequest<ResponseViewModel<ViewInvoiceQuery>>;
    public class ViewInvoiceQueryHandler(IMediator mediator , IRepository<Invoice> repository) : IRequestHandler<ViewInvoiceQuery, ResponseViewModel<ViewInvoiceQuery>>
    {
        private readonly IRepository<Invoice> repository = repository;

        public async Task<ResponseViewModel<ViewInvoiceQuery>> Handle(ViewInvoiceQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponseViewModel<ViewInvoiceQuery>(request);
        }
    }
}
