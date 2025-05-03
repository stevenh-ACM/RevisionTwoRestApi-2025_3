#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

/// <summary>
/// Represents the model for the Credentials Index page.
/// </summary>
public class IndexModel:PageModel
{
    #region ctor
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    public IndexModel(AppDbContext context,ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }
    #endregion

    /// <summary>
    /// Gets or sets the list of credentials.
    /// </summary>
    public IList<Credential> Credentials { get; set; } = default!;

    /// <summary>
    /// Handles the GET request for the Credentials Index page.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnGetAsync()
    {
        Credentials = await _context.Credentials.ToListAsync();
        _logger.LogInformation("Credentials/Index Page");
    }
}