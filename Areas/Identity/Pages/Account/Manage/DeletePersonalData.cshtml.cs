// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the page model for deleting personal data in the ASP.NET Core Identity default UI. This API supports the
/// ASP.NET Core Identity infrastructure and is not intended to be used directly from your code. This API may change or
/// be removed in future releases.
/// </summary>
public class DeletePersonalDataModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<DeletePersonalDataModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonalDataModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    /// <param name="signInManager">The <see cref="SignInManager{TUser}"/> instance used to manage user sign-in operations.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used to log operations and events related to personal data
    /// deletion.</param>
    public DeletePersonalDataModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<DeletePersonalDataModel> logger)
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
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool RequirePassword { get; set; }

    /// <summary>
    /// Handles GET requests for the page and initializes the required data.
    /// </summary>
    /// <remarks>This method retrieves the current user and determines whether a password is required for the
    /// user.  If the user cannot be found, a "Not Found" result is returned.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.  Returns <see langword="NotFound"/> if
    /// the user cannot be loaded; otherwise, returns the page.</returns>
    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        RequirePassword = await _userManager.HasPasswordAsync(user);
        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to delete the currently authenticated user.
    /// </summary>
    /// <remarks>This method deletes the user associated with the current authentication context. If the user
    /// has a password, the password must be provided for verification. Upon successful deletion, the user is signed out
    /// and redirected to the application's root page.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns a <see cref="NotFoundResult"/> if
    /// the user cannot be loaded, a <see cref="PageResult"/> if the password verification fails, or a <see
    /// cref="RedirectResult"/> to the application's root page upon successful deletion.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an unexpected error occurs during the user deletion process.</exception>
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        RequirePassword = await _userManager.HasPasswordAsync(user);
        if (RequirePassword)
        {
            if (!await _userManager.CheckPasswordAsync(user, Input.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }
        }

        var result = await _userManager.DeleteAsync(user);
        var userId = await _userManager.GetUserIdAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Unexpected error occurred deleting user.");
        }

        await _signInManager.SignOutAsync();

        _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

        return Redirect("~/");
    }
}
