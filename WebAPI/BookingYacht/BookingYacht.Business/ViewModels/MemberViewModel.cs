using System;

namespace BookingYacht.Business.ViewModels
{
    public class MemberViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
