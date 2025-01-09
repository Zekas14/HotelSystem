namespace HotelSystem.ViewModels.PaymentProcessing
{
    public class ProcessPaymentViewModel
    {
        public string PaymentMethodNonce { get; set; }
        public decimal Amount { get; set; }
        public int ReservationId { get; set; }
    }
}
