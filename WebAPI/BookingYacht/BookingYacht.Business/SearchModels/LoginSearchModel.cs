using System.ComponentModel.DataAnnotations;

namespace BookingYacht.Business.SearchModels
{
    public class OpenLoginSearchModel
    {
        [Required] public string IdToken { get; set; }
    }
}