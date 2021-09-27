using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Business
    {
        public Business()
        {
            BusinessTours = new HashSet<BusinessTour>();
            Trips = new HashSet<Trip>();
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }

        public virtual ICollection<BusinessTour> BusinessTours { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
