// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for setting a password in the ASP.NET Core Identity default UI.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly  from your code. It provides functionality for users to set a password if they do not already have
/// one.  This API may change or be removed in future releases.</remarks>
public class SetPasswordModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetPasswordModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to handle user sign-in operations.</param>
    public SetPasswordModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
    [TempData]
    public string StatusMessage { get; set; }

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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Handles GET requests for the page and determines the appropriate response based on the user's password status.
    /// </summary>
    /// <remarks>If the user does not exist, a "Not Found" result is returned. If the user has a password, the
    /// method redirects to the "Change Password" page. Otherwise, the current page is displayed.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns <see cref="NotFoundResult"/> if
    /// the user cannot be loaded, <see cref="RedirectToPageResult"/> if the user has a password, or the current page if
    /// the user does not have a password.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (hasPassword)
        {
            return RedirectToPage("./ChangePassword");
        }

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to set a new password for the current user.
    /// </summary>
    /// <remarks>This method validates the model state and attempts to set a new password for the currently
    /// authenticated user. If the operation succeeds, the user's sign-in session is refreshed, and the user is
    /// redirected to the current page. If the operation fails, validation errors are added to the model state, and the
    /// page is re-rendered.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns the current page if the model
    /// state is invalid or if the password update fails. Redirects to the current page upon successful password update.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            foreach (var error in addPasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your password has been set.";

        return RedirectToPage();
    }
}
