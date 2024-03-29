﻿#nullable enable
using System;

namespace BookingYacht.Business.ViewModels
{
    public class AgencyViewModels
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Status { get; set; }
        public string PhotoUrl { get; set; }
        public string Uid { get; set; }
    }
}