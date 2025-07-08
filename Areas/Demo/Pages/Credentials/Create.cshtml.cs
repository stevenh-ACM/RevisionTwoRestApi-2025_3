#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;

using Credential = RevisionTwoApp.RestApi.Models.Credential;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

#region CreateModel
/// <summary>
/// Represents the model for creating a new credential in the application.
/// </summary>
public class CreateModel(AppDbContext context, ILogger<CreateModel> logger):PageModel
{
    #region ctor
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<CreateModel> _logger = logger;
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the credential being created.
    /// </summary>
    [BindProperty]
    public Credential credential { get; set; } = default!;
    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests for the page.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that renders the current page.</returns>
    public IActionResult OnGet( )
    {
        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request for the page.
    /// </summary>
    /// <remarks>This method processes form submissions and performs validation on the model state. If the
    /// model state is invalid, the method returns the current page to allow the user to correct errors. If the model
    /// state is valid, the method saves the submitted data to the database and redirects to the index page.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Returns the current page if the model
    /// state is invalid. Redirects to the index page upon successful processing.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        _context.Attach(credential).State = EntityState.Added;

        if (!ModelState.IsValid)
        {
            var errorMessage = $"Create: No credential exists. Please create at least one!";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        var infoMessage = $"Create: Creating credential {credential}!";
        _logger.LogInformation(infoMessage);    

        _context.Credentials.Add(credential);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
    #endregion
}
#endregion