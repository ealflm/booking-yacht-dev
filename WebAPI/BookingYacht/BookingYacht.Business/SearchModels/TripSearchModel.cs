using System;
using BookingYacht.Business.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.SearchModels
{
    public class TripSearchModel
    {
        public Guid? IdBusiness { get; set; }
        public Guid? IdVehicle { get; set; }
        public Status Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}
