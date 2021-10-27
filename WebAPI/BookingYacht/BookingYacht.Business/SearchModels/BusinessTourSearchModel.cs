using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class BusinessTourSearchModel
    {
        public Guid? IdBusiness { get; set; }
        public Guid? IdTour { get; set; }
        public DateTime? Time { get; set; }
        public Status? Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}