// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for generating new two-factor authentication (2FA) recovery codes.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for generating new recovery codes for users who have 2FA
/// enabled.</remarks>
public class GenerateRecoveryCodesModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<GenerateRecoveryCodesModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateRecoveryCodesModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts and generate recovery codes.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used to log operations and events related to recovery code
    /// generation.</param>
    public GenerateRecoveryCodesModel(
        UserManager<IdentityUser> userManager,
        ILogger<GenerateRecoveryCodesModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string[] RecoveryCodes { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Handles GET requests to prepare the page for generating recovery codes for a user with two-factor authentication
    /// enabled.
    /// </summary>
    /// <remarks>This method retrieves the currently authenticated user and verifies that two-factor
    /// authentication is enabled for the user. If the user is not found, a <see cref="NotFoundResult"/> is returned. If
    /// two-factor authentication is not enabled, an  <see cref="InvalidOperationException"/> is thrown.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns the page if the user is valid
    /// and has two-factor authentication enabled; otherwise, returns a <see cref="NotFoundResult"/> or throws an
    /// exception.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the user does not have two-factor authentication enabled.</exception>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        if (!isTwoFactorEnabled)
        {
            throw new InvalidOperationException($"Cannot generate recovery codes for user because they do not have 2FA enabled.");
        }

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to generate new two-factor authentication (2FA) recovery codes for the current
    /// user.
    /// </summary>
    /// <remarks>This method retrieves the currently authenticated user and generates a new set of recovery
    /// codes for two-factor authentication. Recovery codes are used as a backup mechanism when the user cannot access
    /// their primary 2FA method. The user must have 2FA enabled for recovery codes to be generated.</remarks>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  The result is an <see
    /// cref="IActionResult"/> that redirects to the page displaying the newly generated recovery codes.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the user does not have two-factor authentication enabled.</exception>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        var userId = await _userManager.GetUserIdAsync(user);
        if (!isTwoFactorEnabled)
        {
            throw new InvalidOperationException($"Cannot generate recovery codes for user as they do not have 2FA enabled.");
        }

        var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        RecoveryCodes = recoveryCodes.ToArray();

        _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
        StatusMessage = "You have generated new recovery codes.";
        return RedirectToPage("./ShowRecoveryCodes");
    }
}
