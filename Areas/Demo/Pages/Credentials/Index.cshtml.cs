#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

[Authorize]

/// <summary>
/// Represents the model for the Credentials Index page.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger instance.</param>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    #region ctor   
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<IndexModel> _logger = logger;

    #endregion

    #region methods
    /// <summary>
    /// Gets or sets the list of credentials.
    /// </summary>
    public List<Credential> credentials { get; set; } = default!;

    /// <summary>
    /// Handles the GET request for the Credentials Index page.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnGetAsync()
    {
        credentials = await _context.Credentials.ToListAsync();
        if (credentials is null)
        {
            var errorMessage = $@"Index: No Credentials found - create a credential";
            _logger.LogError(errorMessage);

            RedirectToPage("./Create");
        }
        else 
        {
            var infoMessage = $@"Index: Display Page";
            _logger.LogInformation(infoMessage);
        }
    }
    #endregion
}
