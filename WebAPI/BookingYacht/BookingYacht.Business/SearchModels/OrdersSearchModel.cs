using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class OrdersSearchModel
    {
        public Guid? Id { get; set; }
        public string AgencyName { get; set; }
        public int? QuantityOfPerson { get; set; }
        public float? TotalPrice { get; set; }
        public Guid? IdAgency { get; set; }
        public int Paging { get; set; }
        public Status? Status { get; set; }
    }
}