// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the page model for logging in using a recovery code as part of two-factor authentication.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for handling two-factor authentication using recovery codes,
/// including validating the recovery code and managing the user's authentication state.</remarks>
public class LoginWithRecoveryCodeModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWithRecoveryCodeModel"/> class.
    /// </summary>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> used to manage user sign-in operations.</param>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> used to manage user-related operations such as account recovery.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> used to log diagnostic and operational information.</param>
    public LoginWithRecoveryCodeModel(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ILogger<LoginWithRecoveryCodeModel> logger)
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
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }

    /// <summary>
    /// Handles the GET request for the two-factor authentication page.
    /// </summary>
    /// <remarks>This method ensures that the user has completed the username and password authentication step
    /// before accessing the two-factor authentication page. If the user is not authenticated, an  exception is thrown.
    /// The method sets the return URL for redirection after successful authentication.</remarks>
    /// <param name="returnUrl">The URL to redirect to after successful authentication. Defaults to <see langword="null"/>.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the page rendering.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the two-factor authentication user cannot be loaded.</exception>
    public async Task<IActionResult> OnGetAsync(string returnUrl = null)
    {
        // Ensure the user has gone through the username & password screen first
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            throw new InvalidOperationException($"Unable to load two-factor authentication user.");
        }

        ReturnUrl = returnUrl;

        return Page();
    }

    /// <summary>
    /// Handles the POST request for two-factor authentication using a recovery code.
    /// </summary>
    /// <remarks>This method validates the recovery code provided by the user and attempts to sign them in
    /// using the recovery code. If the recovery code is invalid, the method adds a model error and returns the current
    /// page. If the user account is locked, the method redirects to the lockout page.</remarks>
    /// <param name="returnUrl">The URL to redirect to after successful authentication. If <see langword="null"/>, the user is redirected to the
    /// application's root.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns a page if the model state is
    /// invalid or the recovery code is incorrect. Redirects to the specified <paramref name="returnUrl"/> or the
    /// application's root upon successful authentication. Redirects to the lockout page if the user account is locked.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the two-factor authentication user cannot be loaded.</exception>
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            throw new InvalidOperationException($"Unable to load two-factor authentication user.");
        }

        var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

        var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

        var userId = await _userManager.GetUserIdAsync(user);

        if (result.Succeeded)
        {
            _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
        }
        else
        {
            _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
            ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
            return Page();
        }
    }
}
