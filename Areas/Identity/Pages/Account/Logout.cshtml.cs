// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the model for handling user logout functionality in a Razor Page.
/// </summary>
/// <remarks>This class provides the logic for signing out the currently authenticated user and optionally 
/// redirecting them to a specified URL or the default page. It is designed to be used in Razor Pages  that require
/// logout functionality.</remarks>
public class LogoutModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutModel"/> class.
    /// </summary>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> used to manage user sign-in and sign-out operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> used to log information related to logout operations.</param>
    public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    /// Handles the HTTP POST request to log out the current user and redirect to the specified URL or the default page.
    /// </summary>
    /// <remarks>This method signs out the current user and ensures that the browser performs a new request to
    /// update the user's identity.</remarks>
    /// <param name="returnUrl">The URL to redirect to after logging out. If null, the user is redirected to the default page.</param>
    /// <returns>An <see cref="IActionResult"/> representing the redirection to the specified URL or the default page.</returns>
    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToPage();
        }
    }
}
