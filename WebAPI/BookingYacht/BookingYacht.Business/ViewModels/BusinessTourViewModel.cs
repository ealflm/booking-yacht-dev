using System;
using System.Collections.Generic;
using BookingYacht.Business.Enum;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.ViewModels
{
    public class BusinessTourViewModel
    {
        public Guid id { get; set; }
        public Guid idBusiness { get; set; }
        public Guid idTour { get; set; }
        public int Status { get; set; }
        public BookingYacht.Data.Models.Business IdBusinessNavigation { get; set; }
        public Tour IdTourNavigation { get; set; }
        public List<TicketType> TicketTypes { get; set; }
        public List<TripViewModel> Trips { get; set; }
    }
}