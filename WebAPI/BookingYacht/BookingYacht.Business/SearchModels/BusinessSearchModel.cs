using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.SearchModels
{
    public class BusinessSearchModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}
