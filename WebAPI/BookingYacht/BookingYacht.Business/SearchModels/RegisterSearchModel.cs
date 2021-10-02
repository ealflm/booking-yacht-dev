using System.ComponentModel.DataAnnotations;

namespace BookingYacht.Business.SearchModels
{
    public class RegisterSearchModel
    {
        public string Name { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
