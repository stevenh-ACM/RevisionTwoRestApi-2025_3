// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for resetting the authenticator key in an ASP.NET Core Identity application.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for resetting the authenticator app key associated with a
/// user account.</remarks>
public class ResetAuthenticatorModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<ResetAuthenticatorModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetAuthenticatorModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to handle user sign-in operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used to log diagnostic and operational information.</param>
    public ResetAuthenticatorModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<ResetAuthenticatorModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Handles GET requests for the page and retrieves the current user.
    /// </summary>
    /// <remarks>If the user cannot be found, the method returns a <see cref="NotFoundResult"/>  with an
    /// appropriate error message. Otherwise, it returns the page.</remarks>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.  Returns a <see cref="PageResult"/>
    /// if the user is successfully retrieved, or  a <see cref="NotFoundResult"/> if the user cannot be found.</returns>
    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to reset the user's authenticator app key and disable two-factor authentication.
    /// </summary>
    /// <remarks>This method performs the following actions: <list type="bullet"> <item><description>Retrieves
    /// the currently signed-in user.</description></item> <item><description>Disables two-factor authentication for the
    /// user.</description></item> <item><description>Resets the user's authenticator app key.</description></item>
    /// <item><description>Logs the operation for auditing purposes.</description></item> <item><description>Refreshes
    /// the user's sign-in session.</description></item> <item><description>Sets a status message indicating that the
    /// authenticator app key has been reset.</description></item> </list> After completing these actions, the method
    /// redirects the user to the page for configuring a new authenticator app key.</remarks>
    /// <returns>An <see cref="IActionResult"/> that redirects the user to the page for enabling the authenticator.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await _userManager.SetTwoFactorEnabledAsync(user, false);
        await _userManager.ResetAuthenticatorKeyAsync(user);
        var userId = await _userManager.GetUserIdAsync(user);
        _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

        return RedirectToPage("./EnableAuthenticator");
    }
}
