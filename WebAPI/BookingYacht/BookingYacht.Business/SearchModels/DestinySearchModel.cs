using System;

namespace BookingYacht.Business.SearchModels
{
    public class DestinySearchModel
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public Guid? IdPlaceType { get; set; }  
        public int? Status { get; set; }
        
    }
}