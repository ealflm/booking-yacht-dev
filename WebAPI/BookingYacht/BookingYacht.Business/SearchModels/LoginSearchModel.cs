using System.ComponentModel.DataAnnotations;

namespace BookingYacht.Business.SearchModels
{
    public class LoginSearchModel
    {
        [Required, EmailAddress, StringLength(5)]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public string IdToken { get; set; }
    }
}
