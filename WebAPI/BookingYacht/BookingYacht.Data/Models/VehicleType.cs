﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
