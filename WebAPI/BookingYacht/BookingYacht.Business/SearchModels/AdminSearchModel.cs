using BookingYacht.Business.Enum;
using System;

namespace BookingYacht.Business.SearchModels
{
    public class AdminSearchModel
    {
        public Guid? Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        //public byte[] Password { get; set; }
        //public byte[] Salt { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public Status Status { get; set; }
        public int Page { get; set; }
        public int AmountItem { get; set; }

    }
}
