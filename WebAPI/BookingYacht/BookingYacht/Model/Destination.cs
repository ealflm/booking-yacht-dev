﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class Destination
    {
        public Destination()
        {
            DestinationTours = new HashSet<DestinationTour>();
        }

        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid? IdPlaceType { get; set; }

        public virtual PlaceType IdPlaceTypeNavigation { get; set; }
        public virtual ICollection<DestinationTour> DestinationTours { get; set; }
    }
}
