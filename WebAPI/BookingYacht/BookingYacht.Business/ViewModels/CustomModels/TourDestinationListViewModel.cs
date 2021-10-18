using System;
using System.Collections.Generic;
using BookingYacht.Business.Enum;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.ViewModels.CustomModels
{
    public class TourDestinationListViewModel    
    {
        public Guid IdTour { get; set; }
        public List<TourDestinationViewModel> Destination { get; set; } = new();
    }
}