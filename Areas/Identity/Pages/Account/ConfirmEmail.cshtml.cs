// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the model for the email confirmation page in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for handling email confirmation requests, including
/// validating user identity and confirmation codes.</remarks>
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts and perform operations such as email
    /// confirmation.</param>
    public ConfirmEmailModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }
    /// <summary>
    /// Handles the GET request for email confirmation.
    /// </summary>
    /// <remarks>This method decodes the provided confirmation code and attempts to confirm the user's email
    /// address. If the confirmation succeeds, a success message is displayed; otherwise, an error message is
    /// shown.</remarks>
    /// <param name="userId">The unique identifier of the user whose email is being confirmed. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The email confirmation code, encoded in Base64 URL format. Cannot be <see langword="null"/>.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Redirects to the Index page if <paramref
    /// name="userId"/> or <paramref name="code"/> is <see langword="null"/>. Returns a NotFound result if the user
    /// cannot be loaded. Returns the current page with a status message indicating success or failure of the email
    /// confirmation.</returns>
    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
        return Page();
    }
}
