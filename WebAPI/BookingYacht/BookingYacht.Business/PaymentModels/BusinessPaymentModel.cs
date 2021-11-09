using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.PaymentModels
{
    public class BusinessPaymentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string VnpTmnCode { get; set; }
        public string VnpHashSecret { get; set; }
        public List<BusinessTourPaymentModel> BusinessTours { get; set; }
        public double TotalPrice { get; set; }
    }
}