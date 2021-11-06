using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.ViewModels
{

    public class OrderCreateModel
    {
        public int? QuantityOfPerson { get; set; }
        public double? TotalPrice { get; set; }
        public Guid IdAgency { get; set; }
        public Guid IdTrip { get; set; }
        public DateTime? OrderDate { get; set; }
    }
    public class OrdersViewModel
    {
        public Guid Id { get; set; }
        public string TourName { get; set; }
        public string AgencyName { get; set; }
        public int QuantityOfPerson { get; set; }
        public double TotalPrice { get; set; }
        public Guid IdAgency { get; set; }
        public Status Status { get; set; }
        public Guid IdTrip { get; set; }
        public DateTime OrderDate { get; set; }
        public AgencyViewModels AgencyViewModels { get; set; }
    }
}