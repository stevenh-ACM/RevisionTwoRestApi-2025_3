#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the model for editing credentials in the Razor Page.
/// </summary>
/// <param name="context">The database context.</param>
public class EditModel(AppDbContext context):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="EditModel"/> class.
    /// </summary>
    private readonly AppDbContext _context = context;
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the credential being edited.
    /// </summary>
    [BindProperty]
    public Credential Credentials { get; set; } = default!;
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request to retrieve the credential for editing.
    /// </summary>
    /// <param name="id">The ID of the credential to edit.</param>
    /// <returns>The page result or a not found result.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if (credential == null)
        {
            return NotFound();
        }
        Credentials = credential;
        return Page();
    }

    /// <summary>
    /// Handles the POST request to update the credential.
    /// </summary>
    /// <returns>A redirect to the index page or the current page if validation fails.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Credentials).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CredentialExists(Credentials.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    /// <summary>
    /// Checks if a credential with the specified ID exists.
    /// </summary>
    /// <param name="id">The ID of the credential.</param>
    /// <returns>True if the credential exists; otherwise, false.</returns>
    private bool CredentialExists(int id)
    {
        return _context.Credentials.Any(e => e.Id == id);
    }
    #endregion
}
