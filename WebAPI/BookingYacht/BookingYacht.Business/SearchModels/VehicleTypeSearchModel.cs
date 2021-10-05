using System;

namespace BookingYacht.Business.SearchModels
{
    public class VehicleTypeSearchModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        
        public int Paging { get; set; }
    }
}