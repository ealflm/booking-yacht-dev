using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Admin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
    }
}
