#nullable enable

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RevisionTwoApp.RestApi.Models;

/// <summary>
///     Add profile data for application users by adding properties to the ApiDemoUser class
/// </summary>
public class DemoUser:IdentityUser
{
    /// <summary>
    ///     Gets or sets the full name of the user.
    /// </summary>
    [PersonalData]
    [StringLength(35)]
    public string? FullName { get; set; }

    /// <summary>
    ///     Gets or sets the email address of the user.
    /// </summary>
    [PersonalData]
    [DataType(DataType.EmailAddress)]
    public string? EmailAddress { get; set; }

    /// <summary>
    ///     Gets or sets the date of birth of the user.
    /// </summary>
    [PersonalData]
    public DateTime DOB { get; set; }
}

