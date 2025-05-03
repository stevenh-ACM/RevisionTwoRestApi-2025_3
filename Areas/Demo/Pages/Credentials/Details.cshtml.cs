#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the model for the details page of credentials.
/// </summary>
public class DetailsModel:PageModel
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DetailsModel"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets or sets the credential details.
    /// </summary>
    public Credential Credentials { get; set; } = default!;

    /// <summary>
    /// Handles the GET request to retrieve credential details by ID.
    /// </summary>
    /// <param name="id">The ID of the credential.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
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
}
