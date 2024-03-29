﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BookingYacht.Data.Models
{
    public partial class Ticket
    {
        public Guid Id { get; set; }
        public string NameCustomer { get; set; }
        public string Phone { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdTicketType { get; set; }
        public Guid IdTrip { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual TicketType IdTicketTypeNavigation { get; set; }
        public virtual Trip IdTripNavigation { get; set; }
    }
}