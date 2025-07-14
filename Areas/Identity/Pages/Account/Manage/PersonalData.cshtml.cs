// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;

namespace RevisionTwoApp.RestApi.Areas.Identity.Pages.Account.Manage;
/// <summary>
/// Represents the model for managing personal data in a Razor Page application.
/// </summary>
/// <remarks>This class is used to handle requests related to personal data for the currently authenticated user.
/// It provides functionality to retrieve the user's information and display the corresponding page.</remarks>
public class PersonalDataModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<PersonalDataModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalDataModel"/> class.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance used to manage user identities.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used for logging operations.</param>
    public PersonalDataModel(
        UserManager<IdentityUser> userManager,
        ILogger<PersonalDataModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    /// Handles GET requests for the page and retrieves the current user.
    /// </summary>
    /// <remarks>If the user cannot be found, the method returns a <see cref="NotFoundResult"/> with an error
    /// message. Otherwise, it prepares the page for rendering.</remarks>
    /// <returns>A <see cref="PageResult"/> if the user is successfully retrieved; otherwise, a <see cref="NotFoundResult"/>.</returns>
    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        return Page();
    }
}
