using System;

namespace BookingYacht.Business.ViewModels
{
    public class VehicleModel
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private int Seat { get; set; }
        private string Description { get; set; }
        private int Status { get; set; }
    }
}