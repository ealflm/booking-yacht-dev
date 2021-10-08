using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class OrdersSearchModel
    {
        public string AgencyName { get; set; }
        public int? QuantityOfPerson { get; set; }
        public float? TotalPrice { get; set; }
        public Guid? IdAgency { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
        public Status? Status { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}