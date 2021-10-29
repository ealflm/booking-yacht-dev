using BookingYacht.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.PaymentModels
{
   public class TripPaymentModel
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid IdBusinessTour { get; set; }
        public Guid IdVehicle { get; set; }
        public int Status { get; set; }
        public int? AmountTicket { get; set; }
        public List<Ticket> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
