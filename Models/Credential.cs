#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionTwoApp.RestApi.Models;

/// <summary>
///     Add profile data for application users by adding properties to the ApiDemoUser class
/// </summary>
public class Credential
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Selected")]
    public bool IsChecked { get; set; }

    [Required]
    [Display(Name = "ERP Instance URL")]
    [StringLength(256,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 5)]
    [DataType(DataType.Url)]
    public string? SiteUrl { get; set; }

    [Required]
    [Display(Name = "ERP User Name")]
    //[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
    //[RegularExpression(@"(^(([A-Za-z])+){2,}(([0-9])+)?$)|^[A-Za-z]{2}$|(^(([A-Za-z])+){1,}((([0-9])+){2,})$)")] //alpha numeric only
    public string? UserName { get; set; }

    [Required]
    [StringLength(100,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 3)]
    //[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$")] //1 number 0-9; 1 uppercase; 1 lowercase; 1 non-alpha numeric; 8-16 chars no space
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    //[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
    //[RegularExpression(@"(^(([A-Za-z])+){2,}(([0-9])+)?$)|^[A-Za-z]{2}$|(^(([A-Za-z])+){1,}((([0-9])+){2,})$)")] //alpha numeric only
    public string? Tenant { get; set; }

    //[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
    //[RegularExpression(@"(^(([A-Za-z])+){2,}(([0-9])+)?$)|^[A-Za-z]{2}$|(^(([A-Za-z])+){1,}((([0-9])+){2,})$)")] //alpha numeric only
    public string? Branch { get; set; }

    //[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
    //[RegularExpression(@"^[a-z]*_[A-Z]*$")]
    public string? Locale { get; set; }
}
