// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the page model for confirming an email change in the ASP.NET Core Identity system.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly  from your code. It provides functionality for handling email change confirmation, including updating
/// the user's  email and username, and refreshing the sign-in session.</remarks>
public class ConfirmEmailChangeModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailChangeModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to handle user sign-in operations.</param>
    public ConfirmEmailChangeModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }
    /// <summary>
    /// Handles the GET request to confirm and update a user's email address.
    /// </summary>
    /// <remarks>This method verifies the provided confirmation code, updates the user's email and username,
    /// and refreshes the user's sign-in session. If the operation fails, an error message is displayed on the page. In
    /// this application, the email and username are treated as identical, so both are updated simultaneously.</remarks>
    /// <param name="userId">The unique identifier of the user whose email is being updated. Cannot be <see langword="null"/>.</param>
    /// <param name="email">The new email address to associate with the user. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The confirmation code used to verify the email change. Cannot be <see langword="null"/>.</param>
    /// <returns>An <see cref="IActionResult"/> that redirects to the index page if any parameter is <see langword="null"/>,
    /// returns a "Not Found" result if the user cannot be loaded, or displays the appropriate page based on the success
    /// or failure of the email update operation.</returns>
    public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
    {
        if (userId == null || email == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ChangeEmailAsync(user, email, code);
        if (!result.Succeeded)
        {
            StatusMessage = "Error changing email.";
            return Page();
        }

        // In our UI email and user name are one and the same, so when we update the email
        // we need to update the user name.
        var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
        if (!setUserNameResult.Succeeded)
        {
            StatusMessage = "Error changing user name.";
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Thank you for confirming your email change.";
        return Page();
    }
}
