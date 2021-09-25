using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            BusinessTours = new HashSet<BusinessTour>();
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public double Price { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }

        public virtual Tour IdTourNavigation { get; set; }
        public virtual ICollection<BusinessTour> BusinessTours { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
