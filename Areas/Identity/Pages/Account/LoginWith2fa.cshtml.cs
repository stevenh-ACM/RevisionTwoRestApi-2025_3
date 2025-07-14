// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the page model for handling two-factor authentication during login in the ASP.NET Core Identity default
/// UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for managing two-factor authentication, including retrieving
/// the authenticated user, validating the  authenticator code, and handling login state.</remarks>
public class LoginWith2faModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<LoginWith2faModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWith2faModel"/> class.
    /// </summary>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> used to manage user sign-in operations.</param>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> used to manage user-related operations such as retrieving user information.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> used to log diagnostic and operational information.</param>
    public LoginWith2faModel(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ILogger<LoginWith2faModel> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }
    }

    /// <summary>
    /// Handles the GET request for the two-factor authentication page.
    /// </summary>
    /// <param name="rememberMe">A value indicating whether the user should be remembered on the current device after authentication.</param>
    /// <param name="returnUrl">The URL to redirect to after successful authentication. If <see langword="null"/>, the default page is used.</param>
    /// <returns>An <see cref="IActionResult"/> representing the rendered two-factor authentication page.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the two-factor authentication user cannot be loaded.</exception>
    public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
    {
        // Ensure the user has gone through the username & password screen first
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            throw new InvalidOperationException($"Unable to load two-factor authentication user.");
        }

        ReturnUrl = returnUrl;
        RememberMe = rememberMe;

        return Page();
    }

    /// <summary>
    /// Handles the POST request for two-factor authentication sign-in.
    /// </summary>
    /// <remarks>This method validates the two-factor authentication code provided by the user and attempts to
    /// sign them in. If the authentication succeeds, the user is redirected to the specified <paramref
    /// name="returnUrl"/>. If the account is locked out, the user is redirected to the lockout page. If the
    /// authentication fails, an error message is added to the model state, and the current page is returned.</remarks>
    /// <param name="rememberMe">A value indicating whether the authentication session should persist across browser restarts. If <see
    /// langword="true"/>, the session will be remembered; otherwise, it will not.</param>
    /// <param name="returnUrl">The URL to redirect to after successful authentication. If <see langword="null"/>, the user will be redirected
    /// to the application's root URL.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. This can be a redirect to the specified
    /// <paramref name="returnUrl"/>, a page indicating a locked-out account, or the current page with an error message
    /// if authentication fails.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the two-factor authentication user cannot be loaded.</exception>
    public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        returnUrl = returnUrl ?? Url.Content("~/");

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            throw new InvalidOperationException($"Unable to load two-factor authentication user.");
        }

        var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

        var userId = await _userManager.GetUserIdAsync(user);

        if (result.Succeeded)
        {
            _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
            return LocalRedirect(returnUrl);
        }
        else if (result.IsLockedOut)
        {
            _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
            return RedirectToPage("./Lockout");
        }
        else
        {
            _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
            ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
            return Page();
        }
    }
}
