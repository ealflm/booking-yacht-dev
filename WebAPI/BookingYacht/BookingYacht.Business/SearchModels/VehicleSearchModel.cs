using System;

namespace BookingYacht.Business.SearchModels
{
    public class VehicleSearchModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? Seat { get; set; }
        public string Descriptions { get; set; }
        public Guid? IdVehicleType { get; set; }
        public Guid? IdBusiness { get; set; }
        public int? Status { get; set; }
        
        public int Paging { get; set; }
    }
}