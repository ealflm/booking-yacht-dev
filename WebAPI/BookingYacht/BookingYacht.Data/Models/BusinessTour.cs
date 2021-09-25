using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class BusinessTour
    {
        public Guid Id { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdTicketType { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }

        public virtual Business IdBusinessNavigation { get; set; }
        public virtual TicketType IdTicketTypeNavigation { get; set; }
        public virtual Tour IdTourNavigation { get; set; }
    }
}
