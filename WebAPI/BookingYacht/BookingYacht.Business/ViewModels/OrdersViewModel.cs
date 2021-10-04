using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.ViewModels
{
    public class OrdersViewModel
    {
        public Guid Id { get; set; }
        public string AgencyName { get; set; }
        public int QuantityOfPerson { get; set; }
        public double TotalPrice { get; set; }
        public Guid IdAgency { get; set; }
        public Status Status { get; set; }
    }
}