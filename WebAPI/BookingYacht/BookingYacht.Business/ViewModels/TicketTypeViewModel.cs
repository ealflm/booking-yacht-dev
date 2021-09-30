﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.ViewModels
{
    public class TicketTypeViewModel
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public double? ServiceFeePecentage { get; set; }
        public Guid IdBusiness { get; set; }
        public Guid IdTour { get; set; }
        public Guid IdBusinessTour { get; set; }
    }
}
