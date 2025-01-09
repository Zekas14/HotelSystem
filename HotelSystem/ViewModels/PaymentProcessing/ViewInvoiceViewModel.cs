namespace HotelSystem.ViewModels.PaymentProcessing
{
    public class ViewInvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public int ReservationId { get; set; }
        public string GuestName { get; set; }
        public string RoomType { get; set; }
        public double AmountPaid { get; set; }
        public double TotalAmount { get; set; }
        public double Remain { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; }
        public string PaymentStatus { get; set; }
    }
}
