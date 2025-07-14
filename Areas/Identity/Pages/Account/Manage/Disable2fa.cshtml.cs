// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for disabling two-factor authentication (2FA) in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly  from your code. It provides functionality for disabling 2FA for the currently authenticated
/// user.</remarks>
public class Disable2faModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<Disable2faModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Disable2faModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used for logging operations.</param>
    public Disable2faModel(
        UserManager<IdentityUser> userManager,
        ILogger<Disable2faModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Handles GET requests to retrieve the current user's information and verify their two-factor authentication (2FA)
    /// status.
    /// </summary>
    /// <remarks>This method checks if the current user exists and whether two-factor authentication (2FA) is
    /// enabled for the user. If the user does not exist, a <see cref="NotFoundResult"/> is returned. If 2FA is not
    /// enabled for the user, an <see cref="InvalidOperationException"/> is thrown.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.  Returns <see cref="PageResult"/> if
    /// the user exists and 2FA is enabled.</returns>
    /// <exception cref="InvalidOperationException">Thrown if two-factor authentication is not enabled for the current user.</exception>
    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!await _userManager.GetTwoFactorEnabledAsync(user))
        {
            throw new InvalidOperationException($"Cannot disable 2FA for user as it's not currently enabled.");
        }

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to disable two-factor authentication (2FA) for the current user.
    /// </summary>
    /// <remarks>This method retrieves the currently authenticated user and disables their two-factor
    /// authentication. If the user cannot be found, a <see cref="NotFoundResult"/> is returned. If an error occurs
    /// while disabling 2FA, an <see cref="InvalidOperationException"/> is thrown. Upon successful completion, the user
    /// is redirected to the Two-Factor Authentication page.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns a redirect to the  Two-Factor
    /// Authentication page upon success, or a <see cref="NotFoundResult"/> if the user cannot be found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an unexpected error occurs while disabling two-factor authentication.</exception>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
        if (!disable2faResult.Succeeded)
        {
            throw new InvalidOperationException($"Unexpected error occurred disabling 2FA.");
        }

        _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(User));
        StatusMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
        return RedirectToPage("./TwoFactorAuthentication");
    }
}
