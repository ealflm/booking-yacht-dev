using System;

namespace BookingYacht.Business.ViewModels
{
    public class TicketTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public double? CommissionFeePercentage { get; set; }
        public double? ServiceFeePercentage { get; set; }
        public Guid IdBusinessTour { get; set; }
    }
}
