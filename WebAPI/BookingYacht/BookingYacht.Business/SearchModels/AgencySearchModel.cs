#nullable enable
using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class AgencySearchModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public Status? Status { get; set; }
        
        public int Page { get; set; }
        
        public int AmountItem { get; set; }
        
    }
}