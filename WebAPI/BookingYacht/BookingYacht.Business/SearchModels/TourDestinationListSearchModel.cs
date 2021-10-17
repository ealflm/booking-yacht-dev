using System;
using System.Collections.Generic;
using BookingYacht.Business.Enum;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.SearchModels
{
    public class TourDestinationListSearchModel
    {
        public Guid IdTour { get; set; }
        public Status? Status { get; set; }
        public List<Destination> Destination { get; set; } = new List<Destination>();
    }
}