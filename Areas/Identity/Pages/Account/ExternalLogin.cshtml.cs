// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the page model for handling external login functionality in ASP.NET Core Identity.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and provides methods and
/// properties for managing external login workflows, such as redirecting to external providers, handling callbacks, and
/// confirming user accounts. It is not intended to be used directly from your code and may change or be removed in
/// future releases.</remarks>
[AllowAnonymous]
public class ExternalLoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ExternalLoginModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalLoginModel"/> class, providing the necessary services for
    /// handling external login functionality.
    /// </summary>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> used to manage user sign-in operations.</param>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> used to manage user-related operations such as creation, deletion, and
    /// retrieval.</param>
    /// <param name="userStore">The <see cref="IUserStore{TUser}"/> used to persist user data.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> used for logging operations and diagnostics.</param>
    /// <param name="emailSender">The <see cref="IEmailSender"/> used to send email notifications.</param>
    public ExternalLoginModel(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        ILogger<ExternalLoginModel> logger,
        IEmailSender emailSender)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _logger = logger;
        _emailSender = emailSender;
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
    public string ProviderDisplayName { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string ErrorMessage { get; set; }

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
        [EmailAddress]
        public string Email { get; set; }
    }
    
    /// <summary>
    /// Handles GET requests and redirects the user to the login page.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that redirects the user to the "./Login" page.</returns>
    public IActionResult OnGet() => RedirectToPage("./Login");

    /// <summary>
    /// Initiates an external login process with the specified authentication provider.
    /// </summary>
    /// <remarks>This method configures the external authentication properties and redirects the user to the 
    /// external login provider's authentication page. The <paramref name="returnUrl"/> parameter  allows specifying a
    /// custom URL to return to after the authentication process.</remarks>
    /// <param name="provider">The name of the external authentication provider to use for login.</param>
    /// <param name="returnUrl">The URL to redirect to after the external login process is completed.  If <see langword="null"/>, the default
    /// redirect URL will be used.</param>
    /// <returns>An <see cref="IActionResult"/> that challenges the specified external authentication provider.</returns>
    public IActionResult OnPost(string provider, string returnUrl = null)
    {
        // Request a redirect to the external login provider.
        var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    /// <summary>
    /// Handles the callback from an external login provider and processes the user's authentication result.
    /// </summary>
    /// <remarks>This method processes the external login callback by verifying the login information, signing
    /// in the user if they already have an account, or prompting them to create an account if they do not. It also
    /// handles errors from the external provider and redirects appropriately.</remarks>
    /// <param name="returnUrl">The URL to redirect to after successful authentication. Defaults to the application's root if not specified.</param>
    /// <param name="remoteError">An error message from the external login provider, if any. If provided, the user is redirected to the login page
    /// with the error message.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the outcome of the authentication process. Redirects to the specified
    /// <paramref name="returnUrl"/> on success, the login page on error, the lockout page if the user is locked out, or
    /// a page prompting the user to create an account if no account exists.</returns>
    public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        if (remoteError != null)
        {
            ErrorMessage = $"Error from external provider: {remoteError}";
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ErrorMessage = "Error loading external login information.";
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }

        // Sign in the user with this external login provider if the user already has a login.
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (result.Succeeded)
        {
            _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
            return LocalRedirect(returnUrl);
        }
        if (result.IsLockedOut)
        {
            return RedirectToPage("./Lockout");
        }
        else
        {
            // If the user does not have an account, then ask the user to create an account.
            ReturnUrl = returnUrl;
            ProviderDisplayName = info.ProviderDisplayName;
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                Input = new InputModel
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                };
            }
            return Page();
        }
    }

    /// <summary>
    /// Handles the confirmation process for external login registration.
    /// </summary>
    /// <remarks>This method is invoked after a user registers using an external login provider. It creates a
    /// new user account, associates the external login information with the account, and sends an email confirmation
    /// link if required. If the external login information cannot be retrieved or the registration fails, the user is
    /// redirected to the login page.</remarks>
    /// <param name="returnUrl">The URL to redirect to after the operation completes. Defaults to the application's root if not specified.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. This may include a redirect to the
    /// specified <paramref name="returnUrl"/>, the login page, or a registration confirmation page.</returns>
    public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        // Get the information about the user from the external login provider
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ErrorMessage = "Error loading external login information during confirmation.";
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }

        if (ModelState.IsValid)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // If account confirmation is required, we need to show the link if we don't have a real email sender
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        ProviderDisplayName = info.ProviderDisplayName;
        ReturnUrl = returnUrl;
        return Page();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="IdentityUser"/> class.
    /// </summary>
    /// <remarks>This method attempts to create an instance of <see cref="IdentityUser"/> using a
    /// parameterless constructor. If <see cref="IdentityUser"/> is abstract or does not have a parameterless
    /// constructor, an  <see cref="InvalidOperationException"/> is thrown.</remarks>
    /// <returns>A new instance of <see cref="IdentityUser"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="IdentityUser"/> is abstract or does not have a parameterless constructor. Ensure that <see
    /// cref="IdentityUser"/> meets these requirements, or override the external login page  located at
    /// <c>/Areas/Identity/Pages/Account/ExternalLogin.cshtml</c>.</exception>
    private IdentityUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<IdentityUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
        }
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<IdentityUser>)_userStore;
    }
}
