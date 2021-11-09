using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.ViewModels
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public string NameCustomer { get; set; }
        public string Phone { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdTicketType { get; set; }
        public Guid IdTrip { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public string Qr { get; set; }
    }
}