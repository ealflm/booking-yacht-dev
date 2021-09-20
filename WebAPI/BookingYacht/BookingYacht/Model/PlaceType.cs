using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class PlaceType
    {
        public PlaceType()
        {
            Destinations = new HashSet<Destination>();
        }

        public Guid Id { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
