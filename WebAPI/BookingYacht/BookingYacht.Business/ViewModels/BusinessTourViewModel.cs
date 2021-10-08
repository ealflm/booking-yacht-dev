using System;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.ViewModels
{
    public class BusinessTourViewModel
    {
        public Guid id { get; set; }
        public Guid idBusiness { get; set; }
        public Guid idTour { get; set; }
        public int Status { get; set; }
    }
}