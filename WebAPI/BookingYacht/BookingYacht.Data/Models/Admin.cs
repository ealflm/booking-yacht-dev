using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Admin
    {
        public Guid Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public int Status { get; set; }
    }
}
