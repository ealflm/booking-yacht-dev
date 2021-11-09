using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class TicketSearchModel
    {
        public string NameCustomer { get; set; }
        public string Phone { get; set; }
        public Guid? IdOrder { get; set; }
        public Guid? IdTicketType { get; set; }
        public Guid? IdTrip { get; set; }

        public double? Price { get; set; }
        public Status? Status { get; set; }

        public int Page { get; set; }

        public int AmountItem { get; set; }
    }
}