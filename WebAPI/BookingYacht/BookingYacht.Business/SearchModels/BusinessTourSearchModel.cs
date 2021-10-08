using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class BusinessTourSearchModel
    {
        public Guid? idBusiness { get; set; }
        public Guid? idTour { get; set; }
        public Status? Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}