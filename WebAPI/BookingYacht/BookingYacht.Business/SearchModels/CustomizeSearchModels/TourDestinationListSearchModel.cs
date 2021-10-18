using System;
using System.Collections.Generic;
using BookingYacht.Business.Enum;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.SearchModels.CustomizeSearchModels
{
    public class TourDestinationListSearchModel
    {
        public Guid IdTour { get; set; }
        public List<Guid> Destinations { get; set; } 
    }
}