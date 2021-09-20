using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class Trip
    {
        public Trip()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public byte[] Time { get; set; }
        public Guid? IdBusiness { get; set; }
        public Guid? IdVehicle { get; set; }

        public virtual Business IdBusinessNavigation { get; set; }
        public virtual Vehicle IdVehicleNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
