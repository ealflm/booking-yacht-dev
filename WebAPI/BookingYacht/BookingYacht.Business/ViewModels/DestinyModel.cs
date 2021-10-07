using System;

namespace BookingYacht.Business.ViewModels
{
    public class DestinyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public Guid IdPlaceType { get; set; }
    }
}