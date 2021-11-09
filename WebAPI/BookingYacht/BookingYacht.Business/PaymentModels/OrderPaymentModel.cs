using BookingYacht.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.PaymentModels
{
    public class OrderPaymentModel
    {
        public Guid Id { get; set; }
        public string AgencyName { get; set; }
        public int QuantityOfPerson { get; set; }
        public double? TotalPrice { get; set; }
        public Guid IdAgency { get; set; }
        public int Status { get; set; }
        public DateTime? DateOrder { get; set; }
        public Guid? IdTrip { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}