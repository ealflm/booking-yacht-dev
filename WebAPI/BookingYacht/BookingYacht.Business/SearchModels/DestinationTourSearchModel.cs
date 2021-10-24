using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class DestinationTourSearchModel
    {
        public Guid? IdDestination { get; set; }
        public Guid? IdTour { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
        public int Order { get; set; }
    }
}
