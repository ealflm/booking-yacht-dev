using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class PlaceType
    {
        public PlaceType()
        {
            Destinations = new HashSet<Destination>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}