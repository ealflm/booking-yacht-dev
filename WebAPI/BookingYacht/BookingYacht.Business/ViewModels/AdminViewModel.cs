using System;
using System.ComponentModel.DataAnnotations;

namespace BookingYacht.Business.ViewModels
{
    public class AdminViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
