// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for changing a user's password in the ASP.NET Core Identity system.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides functionality for handling password change operations, including
/// validation and updating the user's password.</remarks>
public class ChangePasswordModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<ChangePasswordModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to manage user sign-in operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used for logging operations.</param>
    public ChangePasswordModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<ChangePasswordModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

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
    /// <remarks>This method retrieves the currently authenticated user and checks whether they have a
    /// password set. If the user does not exist, a "Not Found" result is returned. If the user does not have a
    /// password, they are redirected to the page for setting a password. Otherwise, the method renders the current
    /// page.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns <see langword="null"/> if the
    /// user is not found, a redirect to the password setup page if the user does not have a password, or the current
    /// page if the user has a password.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            return RedirectToPage("./SetPassword");
        }

        return Page();
    }
    /// <summary>
    /// Handles the HTTP POST request to change the user's password.
    /// </summary>
    /// <remarks>This method validates the model state, retrieves the current user, and attempts to change
    /// their password. If the password change is successful, the user is re-signed in and redirected to the appropriate
    /// page. If the operation fails, validation errors are added to the model state and the current page is
    /// re-displayed.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns the current page if the model
    /// state is invalid or the password change fails. Returns a redirect to another page if the password change
    /// succeeds.</returns>
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

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        _logger.LogInformation("User changed their password successfully.");
        StatusMessage = "Your password has been changed.";

        return RedirectToPage();
    }
}
