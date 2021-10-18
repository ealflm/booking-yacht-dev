using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.VNPay
{
    public class OrderRequest
    {
        public string Ip { get; set; }
        public long Amount { get; set; }
    }
}
