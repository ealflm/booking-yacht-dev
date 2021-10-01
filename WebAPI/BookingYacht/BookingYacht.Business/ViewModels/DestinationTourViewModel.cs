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
        public Guid IdPier { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }
        public int Way { get; set; }
    }
}
