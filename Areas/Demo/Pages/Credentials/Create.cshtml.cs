#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the model for creating a new credential in the application.
/// </summary>
public class CreateModel:PageModel
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModel"/> class.
    /// </summary>
    /// <param name="context">The database context used to interact with the application's data.</param>
    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles GET requests to display the create credential page.
    /// </summary>
    /// <returns>A <see cref="PageResult"/> representing the page to be displayed.</returns>
    public IActionResult OnGet()
    {
        return Page();
    }

    /// <summary>
    /// Gets or sets the credential being created.
    /// </summary>
    [BindProperty]
    public Credential Credentials { get; set; } = default!;

    /// <summary>
    /// Handles POST requests to create a new credential.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> that represents the asynchronous operation.
    /// Redirects to the index page upon successful creation, or redisplays the page if the model state is invalid.
    /// </returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Credentials.Add(Credentials);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
