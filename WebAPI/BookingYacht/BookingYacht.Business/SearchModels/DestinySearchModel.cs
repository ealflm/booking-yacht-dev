using System;

namespace BookingYacht.Business.SearchModels
{
    public class DestinySearchModel
    {

        public string Name { get; set; }

        public string Address { get; set; }
        public Guid? IdPlaceType { get; set; }  
        public int? Status { get; set; }
        
        public int Page { get; set; }
        
        public int AmountItem { get; set; }
        
    }
}