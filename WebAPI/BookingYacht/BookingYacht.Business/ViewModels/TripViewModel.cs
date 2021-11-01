using BookingYacht.Data.Models;
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
        public Guid IdBusinessTour { get; set; }
        public Guid IdVehicle { get; set; }
        public int Status { get; set; }
        public Guid IdTour { get; set; }
        public Guid IdBusiness { get; set; }
        public int? AmountTicket { get; set; }
        public List<Order> Orders { get; set; }
        public Vehicle IdVehicleNavigation { get; set; }
    }
}
