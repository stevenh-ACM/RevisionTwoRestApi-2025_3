// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the model for managing external logins in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for viewing, adding, and removing external logins associated
/// with the current user.</remarks>
public class ExternalLoginsModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserStore<IdentityUser> _userStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalLoginsModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to handle user sign-in operations.</param>
    /// <param name="userStore">The <see cref="IUserStore{TUser}"/> instance used to interact with the underlying user data store.</param>
    public ExternalLoginsModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IUserStore<IdentityUser> userStore)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> OtherLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool ShowRemoveButton { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Handles GET requests to retrieve and display the user's login information.
    /// </summary>
    /// <remarks>This method retrieves the current user's login information, including external logins and
    /// authentication schemes. It also determines whether the "Remove" button should be displayed based on the user's
    /// password hash and the number of logins.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns the page if the user is found,
    /// or a <see cref="NotFoundResult"/> if the user cannot be loaded.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        CurrentLogins = await _userManager.GetLoginsAsync(user);
        OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
            .Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
            .ToList();

        string passwordHash = null;
        if (_userStore is IUserPasswordStore<IdentityUser> userPasswordStore)
        {
            passwordHash = await userPasswordStore.GetPasswordHashAsync(user, HttpContext.RequestAborted);
        }

        ShowRemoveButton = passwordHash != null || CurrentLogins.Count > 1;
        return Page();
    }

    /// <summary>
    /// Removes an external login associated with the current user.
    /// </summary>
    /// <remarks>This method removes the specified external login from the current user's account. If the
    /// operation succeeds, the user's sign-in session is refreshed to reflect the change. If the operation fails, a
    /// status message is set indicating the failure.</remarks>
    /// <param name="loginProvider">The name of the external login provider (e.g., "Google", "Facebook").</param>
    /// <param name="providerKey">The unique key identifying the external login for the specified provider.</param>
    /// <returns>An <see cref="IActionResult"/> that redirects to the current page after the operation completes. If the user is
    /// not found, returns a <see cref="NotFoundResult"/>.</returns>
    public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        if (!result.Succeeded)
        {
            StatusMessage = "The external login was not removed.";
            return RedirectToPage();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "The external login was removed.";
        return RedirectToPage();
    }

    /// <summary>
    /// Initiates the process of linking an external login provider to the current user's account.
    /// </summary>
    /// <remarks>This method clears any existing external cookies to ensure a clean login process before
    /// redirecting the user. The external login provider specified by <paramref name="provider"/> must be properly
    /// configured in the application. After successful authentication, the external login will be linked to the current
    /// user's account.</remarks>
    /// <param name="provider">The name of the external login provider to link. This value must correspond to a configured authentication
    /// provider.</param>
    /// <returns>An <see cref="IActionResult"/> that redirects the user to the external login provider's authentication page.</returns>
    public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
    {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // Request a redirect to the external login provider to link a login for the current user
        var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
        return new ChallengeResult(provider, properties);
    }

    /// <summary>
    /// Handles the callback for linking an external login to the current user account.
    /// </summary>
    /// <remarks>This method processes the external login information provided by the authentication
    /// middleware and attempts to associate it with the currently signed-in user. If the external login is successfully
    /// linked, the user is redirected to the current page with a success message. If the operation fails, an
    /// appropriate error message is displayed, and the user remains on the current page.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns a redirect to the current page if
    /// the external login is successfully added or if an error occurs. Returns <see cref="NotFoundResult"/> if the
    /// current user cannot be loaded.</returns>
    /// <exception cref="InvalidOperationException">Thrown if external login information cannot be loaded.</exception>
    public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var info = await _signInManager.GetExternalLoginInfoAsync(userId);
        if (info == null)
        {
            throw new InvalidOperationException($"Unexpected error occurred loading external login info.");
        }

        var result = await _userManager.AddLoginAsync(user, info);
        if (!result.Succeeded)
        {
            StatusMessage = "The external login was not added. External logins can only be associated with one account.";
            return RedirectToPage();
        }

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        StatusMessage = "The external login was added.";
        return RedirectToPage();
    }
}
