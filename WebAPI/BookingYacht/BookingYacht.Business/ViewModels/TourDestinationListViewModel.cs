using System;
using System.Collections.Generic;
using BookingYacht.Business.Enum;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.ViewModels
{
    public class TourDestinationListViewModel    
    {
        public Guid IdTour { get; set; }
        public Status Status { get; set; }
        public List<DestinyViewModel> Destination { get; set; } = new List<DestinyViewModel>();
    }
}