using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.ViewModels
{
    public class TourViewModel
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public string Descriptions { get; set; }
        public int Status { get; set; }
        public string ImageLink { get; set; }
        public List<DestinationTourViewModel> DestinationTours { get; set; }
    }
}
