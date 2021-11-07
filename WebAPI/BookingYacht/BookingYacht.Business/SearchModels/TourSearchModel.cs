using BookingYacht.Business.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.SearchModels
{
    public class TourSearchModel
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public Status Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}
