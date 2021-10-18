using System;

namespace BookingYacht.Business.ViewModels.CustomModels
{
    public class TourDestinationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }
    }
}