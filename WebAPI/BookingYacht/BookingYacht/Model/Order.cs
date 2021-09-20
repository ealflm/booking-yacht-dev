using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Model
{
    public partial class Order
    {
        public Order()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public string AgencyName { get; set; }
        public int? QuantityOfPerson { get; set; }
        public double? TotalPrice { get; set; }
        public Guid? IdAgency { get; set; }

        public virtual Agency IdAgencyNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
