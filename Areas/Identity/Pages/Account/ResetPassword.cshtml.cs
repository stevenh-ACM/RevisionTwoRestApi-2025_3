// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account;
/// <summary>
/// Represents the model for handling password reset functionality in the ASP.NET Core Identity framework.
/// </summary>
/// <remarks>This class is part of the ASP.NET Core Identity default UI infrastructure and is not intended to be
/// used directly from your code. It provides the necessary properties and methods to facilitate password reset
/// operations, including validation and user interaction.</remarks>
public class ResetPasswordModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordModel"/> class.
    /// </summary>
    /// <remarks>The <paramref name="userManager"/> parameter is required to perform operations related to
    /// user account management, such as resetting passwords.</remarks>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user accounts.</param>
    public ResetPasswordModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        public string Code { get; set; }

    }

    /// <summary>
    /// Handles GET requests for the password reset page.
    /// </summary>
    /// <param name="code">The base64 URL-encoded password reset code. This parameter is optional but must be provided for the operation to
    /// succeed.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.  Returns <see langword="BadRequest"/>
    /// if <paramref name="code"/> is <see langword="null"/>.  Otherwise, returns the password reset page with the
    /// decoded reset code.</returns>
    public IActionResult OnGet(string code = null)
    {
        if (code == null)
        {
            return BadRequest("A code must be supplied for password reset.");
        }
        else
        {
            Input = new InputModel
            {
                Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
            };
            return Page();
        }
    }

    /// <summary>
    /// Handles the HTTP POST request for resetting a user's password.
    /// </summary>
    /// <remarks>This method validates the model state and attempts to reset the password for the user
    /// associated with the provided email address. If the operation succeeds, the user is redirected to the password
    /// reset confirmation page. If the operation fails, validation errors are added to the model state and the current
    /// page is re-rendered.</remarks>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns the current page if the model
    /// state is invalid or if the password reset operation fails. Redirects to the password reset confirmation page if
    /// the operation succeeds.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToPage("./ResetPasswordConfirmation");
        }

        var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
        if (result.Succeeded)
        {
            return RedirectToPage("./ResetPasswordConfirmation");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return Page();
    }
}
