using System;

namespace BookingYacht.Business.SearchModels
{
    public class VehicleTypeSearchModel
    {
        public string Name { get; set; }
        public int? Status { get; set; }
        
        public int Page { get; set; }
        public int AmountItem { get; set; }
        
    }
}