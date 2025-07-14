// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the page model for resending an email confirmation in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for handling email confirmation resends, including
/// validating user input and sending confirmation emails. This API may change or be removed in future
/// releases.</remarks>
[AllowAnonymous]
public class ResendEmailConfirmationModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendEmailConfirmationModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="emailSender">The <see cref="IEmailSender"/> instance used to send email confirmations.</param>
    public ResendEmailConfirmationModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
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
    /// Handles GET requests for the page.
    /// </summary>
    /// <remarks>This method is invoked automatically during the page's lifecycle to process GET requests.
    /// Override this method to implement custom logic for handling GET requests.</remarks>
    public void OnGet()
    {
    }

    /// <summary>
    /// Handles the HTTP POST request to send a verification email for email confirmation.
    /// </summary>
    /// <remarks>This method validates the model state and attempts to find the user associated with the
    /// provided email address. If the user exists, it generates an email confirmation token, constructs a callback URL,
    /// and sends a verification email. If the user does not exist, a generic message is displayed indicating that a
    /// verification email has been sent.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns the current page if the model
    /// state is invalid or after sending the verification email.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { userId = userId, code = code },
            protocol: Request.Scheme);
        await _emailSender.SendEmailAsync(
            Input.Email,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        return Page();
    }
}
