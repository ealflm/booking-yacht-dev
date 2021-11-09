using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class DestinationTour
    {
        public Guid Id { get; set; }
        public Guid IdDestination { get; set; }
        public Guid IdTour { get; set; }
        public int Order { get; set; }

        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual Tour IdTourNavigation { get; set; }
    }
}