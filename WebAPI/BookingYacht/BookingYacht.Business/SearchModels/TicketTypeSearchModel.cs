using BookingYacht.Business.Enum;
using System;

namespace BookingYacht.Business.SearchModels
{
    public class TicketTypeSearchModel
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public double? ServiceFeePercentage { get; set; }
        public Guid? IdBusinessTour { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
        public Status Status { get; set; }
    }
}
