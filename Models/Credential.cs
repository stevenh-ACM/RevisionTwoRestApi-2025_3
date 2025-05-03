#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionTwoApp.RestApi.Models;

#region Credential
/// <summary>
/// Represents the credentials required for accessing an ERP system.
/// </summary>
public class Credential
{
    #region Credential
    /// <summary>
    /// Gets or sets the unique identifier for the credential.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the credential is selected.
    /// </summary>
    [Display(Name = "Selected")]
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets or sets the ERP instance URL.
    /// </summary>
    [Required]
    [Display(Name = "ERP Instance URL")]
    [StringLength(256,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 5)]
    [DataType(DataType.Url)]
    public string? SiteUrl { get; set; }

    /// <summary>
    /// Gets or sets the ERP user name.
    /// </summary>
    [Required]
    [Display(Name = "ERP User Name")]
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password for the ERP user.
    /// </summary>
    [Required]
    [StringLength(100,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 3)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the tenant associated with the credential.
    /// </summary>
    public string? Tenant { get; set; }

    /// <summary>
    /// Gets or sets the branch associated with the credential.
    /// </summary>
    public string? Branch { get; set; }

    /// <summary>
    /// Gets or sets the locale associated with the credential.
    /// </summary>
    public string? Locale { get; set; }

    #endregion
}
#endregion
