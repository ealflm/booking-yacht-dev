using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public string Size { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
