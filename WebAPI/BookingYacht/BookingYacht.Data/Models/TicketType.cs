using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public double? CommissionFeePercentage { get; set; }
        public double? ServiceFeePercentage { get; set; }
        public Guid IdBusinessTour { get; set; }

        public virtual BusinessTour IdBusinessTourNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
