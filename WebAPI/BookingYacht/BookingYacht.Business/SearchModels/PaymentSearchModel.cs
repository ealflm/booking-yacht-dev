using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.SearchModels
{
    public class PaymentSearchModel
    {
        public DateTime DateTime { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}
