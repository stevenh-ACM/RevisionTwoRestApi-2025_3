// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for downloading personal data associated with the current user.
/// </summary>
/// <remarks>This page model provides functionality for users to download their personal data in JSON format. The
/// personal data includes properties marked with the <see cref="PersonalDataAttribute"/> in the  <see
/// cref="IdentityUser"/> class, external login provider keys, and the authenticator key.</remarks>
public class DownloadPersonalDataModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<DownloadPersonalDataModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadPersonalDataModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user-related operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used to log messages related to personal data downloads.</param>
    public DownloadPersonalDataModel(
        UserManager<IdentityUser> userManager,
        ILogger<DownloadPersonalDataModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    /// Handles GET requests for the current page.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="NotFoundResult"/> if
    /// the requested resource is not found.</returns>
    public IActionResult OnGet()
    {
        return NotFound();
    }

    /// <summary>
    /// Handles the HTTP POST request to generate and download the user's personal data as a JSON file.
    /// </summary>
    /// <remarks>This method retrieves the personal data associated with the currently authenticated user,
    /// including properties marked with the <see cref="PersonalDataAttribute"/> in the <see cref="IdentityUser"/>
    /// class, external login provider keys, and the authenticator key. The data is returned as a downloadable JSON
    /// file.</remarks>
    /// <returns>An <see cref="IActionResult"/> that represents the downloadable JSON file containing the user's personal data.
    /// Returns <see cref="NotFoundResult"/> if the user cannot be loaded.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

        // Only include personal data for download
        var personalData = new Dictionary<string, string>();
        var personalDataProps = typeof(IdentityUser).GetProperties().Where(
                        prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
        foreach (var p in personalDataProps)
        {
            personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
        }

        var logins = await _userManager.GetLoginsAsync(user);
        foreach (var l in logins)
        {
            personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
        }

        personalData.Add($"Authenticator Key", await _userManager.GetAuthenticatorKeyAsync(user));

        Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
        return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
    }
}
