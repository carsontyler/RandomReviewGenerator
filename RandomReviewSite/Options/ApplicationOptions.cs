using System.ComponentModel.DataAnnotations;

namespace RandomReviewSite.Options;

public class ApplicationOptions
{
    [Required]
    public string ApiUrl {get;set;} = "";
}