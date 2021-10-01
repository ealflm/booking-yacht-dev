using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class DestinationTour
    {
        public Guid Id { get; set; }
        public Guid IdPier { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }
        public int Way { get; set; }

        public virtual Destination IdPierNavigation { get; set; }
        public virtual Tour IdTourNavigation { get; set; }
    }
}
