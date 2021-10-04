using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdVehicle { get; set; }
        public int Status { get; set; }

        public virtual Business IdBusinessNavigation { get; set; }
        public virtual Vehicle IdVehicleNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
