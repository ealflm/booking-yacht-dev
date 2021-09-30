using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class BusinessTour
    {
        public BusinessTour()
        {
            TicketTypes = new HashSet<TicketType>();
        }

        public Guid Id { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdTour { get; set; }
        public int Status { get; set; }

        public virtual Business IdBusinessNavigation { get; set; }
        public virtual Tour IdTourNavigation { get; set; }
        public virtual ICollection<TicketType> TicketTypes { get; set; }
    }
}
