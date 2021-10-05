﻿#nullable enable
using System;

namespace BookingYacht.Business.SearchModels
{
    public class AgencySearchModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public int? Status { get; set; }
        
        public int Paging { get; set; }
        
    }
}