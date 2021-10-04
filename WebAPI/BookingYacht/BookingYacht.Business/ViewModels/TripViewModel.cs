using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.ViewModels
{
    public class TripViewModel
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdVehicle { get; set; }
        public int Status { get; set; }
    }
}
