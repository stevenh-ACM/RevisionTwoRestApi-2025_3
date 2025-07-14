// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the model for the registration confirmation page in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for confirming user registration via email and generating an
/// email confirmation link. This API may change or be removed in future releases.</remarks>
[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterConfirmationModel"/> class.
    /// </summary>
    /// <param name="userManager">The user manager used to manage user accounts and authentication.</param>
    /// <param name="sender">The email sender used to send confirmation emails.</param>
    public RegisterConfirmationModel(UserManager<IdentityUser> userManager, IEmailSender sender)
    {
        _userManager = userManager;
        _sender = sender;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool DisplayConfirmAccountLink { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string EmailConfirmationUrl { get; set; }

    /// <summary>
    /// Handles GET requests for the email confirmation page.   
    /// </summary>
    /// <remarks>This method generates an email confirmation link for the specified user and displays it on
    /// the page. The confirmation link is only displayed if <c>DisplayConfirmAccountLink</c> is set to <see
    /// langword="true"/>.</remarks>
    /// <param name="email">The email address of the user to confirm. Cannot be null.</param>
    /// <param name="returnUrl">The URL to redirect to after the operation completes. Defaults to the application's root URL if not provided.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.  Returns a redirect to the home page if
    /// <paramref name="email"/> is null,  a "Not Found" result if the user cannot be located, or the confirmation page
    /// if successful.</returns>
    public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
    {
        if (email == null)
        {
            return RedirectToPage("/Index");
        }
        returnUrl = returnUrl ?? Url.Content("~/");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound($"Unable to load user with email '{email}'.");
        }

        Email = email;
        // Once you add a real email sender, you should remove this code that lets you confirm the account
        DisplayConfirmAccountLink = true;
        if (DisplayConfirmAccountLink)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);
        }

        return Page();
    }
}
