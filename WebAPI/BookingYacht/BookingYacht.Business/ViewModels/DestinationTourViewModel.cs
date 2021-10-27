using BookingYacht.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.ViewModels
{
    public class DestinationTourViewModel
    {
        public Guid Id { get; set; }
        public Guid IdDestination { get; set; }
        public Guid IdTour { get; set; }
        public int Order { get; set; }
        public Destination Destination { get; set; }
        public string PlaceType { get; set; }
    }
}
