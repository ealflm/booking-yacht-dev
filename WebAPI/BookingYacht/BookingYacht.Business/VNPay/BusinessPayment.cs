using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.VNPay
{
    public class BusinessPayment
    {
        public string Ip { get; set; }
        public Guid IdBusiness { get; set; }
        public long Amount { get; set; }
        public List<Guid> IdOrders { get; set; }
    }
}
