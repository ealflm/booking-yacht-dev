using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Models
{
    public partial class Member
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
