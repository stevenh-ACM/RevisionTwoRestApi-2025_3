// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the model for managing two-factor authentication settings in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for enabling, disabling, and managing two-factor
/// authentication settings, including recovery codes and remembered devices.</remarks>
public class TwoFactorAuthenticationModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<TwoFactorAuthenticationModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TwoFactorAuthenticationModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to handle user sign-in operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used for logging operations related to two-factor
    /// authentication.</param>
    public TwoFactorAuthenticationModel(
        UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<TwoFactorAuthenticationModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool HasAuthenticator { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public int RecoveryCodesLeft { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public bool Is2faEnabled { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool IsMachineRemembered { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Handles GET requests for the page and initializes user-specific two-factor authentication data.
    /// </summary>
    /// <remarks>This method retrieves the current user and populates properties related to two-factor
    /// authentication, including whether the user has an authenticator configured, whether two-factor authentication is
    /// enabled, whether the current machine is remembered for two-factor authentication, and the number of recovery
    /// codes remaining. If the user cannot be found, the method returns a <see cref="NotFoundResult"/>.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns <see cref="PageResult"/> if the
    /// user is found, or <see cref="NotFoundResult"/> if the user cannot be loaded.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
        Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
        RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to forget the current browser for two-factor authentication.
    /// </summary>
    /// <remarks>This method retrieves the currently authenticated user and marks the browser as forgotten for
    /// two-factor authentication purposes. After this operation, the user will be prompted for their two-factor
    /// authentication code when logging in again from the same browser.</remarks>
    /// <returns>An <see cref="IActionResult"/> that redirects to the current page after successfully forgetting the browser, or
    /// a <see cref="NotFoundResult"/> if the user cannot be loaded.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await _signInManager.ForgetTwoFactorClientAsync();
        StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
        return RedirectToPage();
    }
}
