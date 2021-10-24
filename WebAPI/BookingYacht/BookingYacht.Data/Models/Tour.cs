using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Tour
    {
        public Tour()
        {
            BusinessTours = new HashSet<BusinessTour>();
            DestinationTours = new HashSet<DestinationTour>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int Status { get; set; }
        public string ImageLink { get; set; }

        public virtual ICollection<BusinessTour> BusinessTours { get; set; }
        public virtual ICollection<DestinationTour> DestinationTours { get; set; }
    }
}
