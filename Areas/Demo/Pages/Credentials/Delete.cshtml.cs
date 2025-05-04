#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the model for deleting a credential.
/// </summary>
/// <param name="context">The database context.</param>

public class DeleteModel(AppDbContext context):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteModel"/> class.
    /// </summary>
    private readonly AppDbContext _context = context;
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the credential to be deleted.
    /// </summary>
    [BindProperty]
    public Credential Credentials { get; set; } = default!;
    #endregion

    #region methods

    /// <summary>
    /// Handles the GET request to retrieve the credential for deletion.
    /// </summary>
    /// <param name="id">The ID of the credential to retrieve.</param>
    /// <returns>The page result or a not found result.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var credentials = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);

        if (credentials == null)
        {
            return NotFound();
        }
        else
        {
            Credentials = credentials;
        }
        return Page();
    }

    /// <summary>
    /// Handles the POST request to delete the credential.
    /// </summary>
    /// <param name="id">The ID of the credential to delete.</param>
    /// <returns>A redirect to the index page or a not found result.</returns>
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var credential = await _context.Credentials.FindAsync(id);
        if (credential != null)
        {
            Credentials = credential;
            _context.Credentials.Remove(Credentials);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
    #endregion
}
