using System.ComponentModel.DataAnnotations;

namespace BookingYacht.Business.SearchModels
{
    public class LoginSearchModel
    {
        [Required, EmailAddress] public string EmailAddress { get; set; }

        [Required] public string Password { get; set; }
    }
}