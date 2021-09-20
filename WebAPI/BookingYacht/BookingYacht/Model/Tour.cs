﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class Tour
    {
        public Tour()
        {
            BusinessTours = new HashSet<BusinessTour>();
            DestinationTours = new HashSet<DestinationTour>();
            TicketTypes = new HashSet<TicketType>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }

        public virtual ICollection<BusinessTour> BusinessTours { get; set; }
        public virtual ICollection<DestinationTour> DestinationTours { get; set; }
        public virtual ICollection<TicketType> TicketTypes { get; set; }
    }
}
