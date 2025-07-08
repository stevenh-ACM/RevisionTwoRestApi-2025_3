#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the page model for editing credentials.
/// </summary>
/// <remarks>This class provides functionality to handle GET and POST requests for editing credentials. It
/// interacts with the database context to retrieve and update credential data.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class EditModel(AppDbContext context, ILogger<EditModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="EditModel"/> class.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<EditModel> _logger = logger;
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the credential being edited.
    /// </summary>
    [BindProperty]
    public Credential credential { get; set; } = default!;
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request to retrieve the credential for editing.
    /// </summary>
    /// <param name="id">The ID of the credential to edit.</param>
    /// <returns>The page result or a not found result.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            var errorMessage = $@"Edit: No ID provided, {id} was passed in.";
            _logger.LogError(errorMessage);

            throw new ArgumentNullException(nameof(id));
        }

        var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if (credential is null)
        {
            var errorMessage = $@"Edit: No credential with the Id {id} found.";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        return Page();
    }

    /// <summary>
    /// Handles the POST request to update the credential.
    /// </summary>
    /// <returns>A redirect to the index page or the current page if validation fails.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        _context.Attach(credential).State = EntityState.Modified;

        if (!ModelState.IsValid)
        {
            var errorMessage = $"Create: No credential exists. Please create at least one!";
            _logger.LogError(errorMessage);

            return NotFound();
        }
        
        await _context.SaveChangesAsync();

        var infoMessage = $"Edit: Credential {credential} updated successfully!";
        _logger.LogInformation(infoMessage);

        return RedirectToPage("./Index");
    }

    ///// <summary>
    ///// Checks if a credential with the specified ID exists.
    ///// </summary>
    ///// <param name="id">The ID of the credential.</param>
    ///// <returns>True if the credential exists; otherwise, false.</returns>
    //private bool CredentialExists(int id)
    //{
    //    return _context.Credentials.Any(e => e.Id == id);
    //}
    //#endregion
}
#endregion