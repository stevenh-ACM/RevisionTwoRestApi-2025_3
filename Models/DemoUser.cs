#nullable enable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Models;

/// <summary>
///     Add profile data for application users by adding properties to the ApiDemoUser class
/// </summary>
public class DemoUser:IdentityUser
{
    [PersonalData]
    public string? FullName { get; set; }

    [PersonalData]
    [DataType(DataType.EmailAddress)]
    public string? EmailAddress { get; set; }

}

