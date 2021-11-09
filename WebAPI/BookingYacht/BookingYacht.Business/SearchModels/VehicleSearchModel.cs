using System;

namespace BookingYacht.Business.SearchModels
{
    public class VehicleSearchModel
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public int? YearOfManufacture { get; set; }
        public string WhereProduction { get; set; }
        public int? Seat { get; set; }
        public string Descriptions { get; set; }
        public Guid? IdVehicleType { get; set; }
        public Guid? IdBusiness { get; set; }
        public int? Status { get; set; }

        public int Page { get; set; }

        public int AmountItem { get; set; }
    }
}