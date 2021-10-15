using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Trips = new HashSet<Trip>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public int? YearOfManufacture { get; set; }
        public string WhereProduction { get; set; }
        public int Seat { get; set; }
        public string Descriptions { get; set; }
        public Guid IdVehicleType { get; set; }
        public Guid IdBusiness { get; set; }
        public int Status { get; set; }

        public virtual Business IdBusinessNavigation { get; set; }
        public virtual VehicleType IdVehicleTypeNavigation { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
