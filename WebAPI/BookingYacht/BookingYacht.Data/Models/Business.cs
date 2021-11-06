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
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string VnpTmnCode { get; set; }
        public string VnpHashSecret { get; set; }
        public string FcmToken { get; set; }

        public virtual ICollection<BusinessTour> BusinessTours { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
