﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.SearchModels
{
    public class PlaceTypeSearchModel
    {
        public string Name { get; set; }
        public Status Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }
    }
}