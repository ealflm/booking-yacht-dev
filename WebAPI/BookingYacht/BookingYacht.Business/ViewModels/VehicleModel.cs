using System;

namespace BookingYacht.Business.ViewModels
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Seat { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}