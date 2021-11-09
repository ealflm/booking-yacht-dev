using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Orders = new HashSet<Order>();
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid IdBusinessTour { get; set; }
        public Guid IdVehicle { get; set; }
        public int? AmountTicket { get; set; }
        public int Status { get; set; }

        public virtual BusinessTour IdBusinessTourNavigation { get; set; }
        public virtual Vehicle IdVehicleNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}