using BookingYacht.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.PaymentModels
{
  public   class BusinessTourPaymentModel
    {
        public Guid Id { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }
        public Tour Tour { get; set; }
        public List<TripPaymentModel> Trips { get; set; }
        public double TotalPrice { get; set; }
    }
}
