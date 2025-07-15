#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Microsoft.AspNetCore.Authorization;

using Task = System.Threading.Tasks.Task;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

[Authorize]

#region IndexModel
/// <summary>
/// Represents the model for the Credentials Index page.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger instance.</param>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<IndexModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _className = nameof(IndexModel);
    #endregion ctor

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
            var errorMessage = $@"{_className}: No Credentials found - create a credential";
            _logger.LogError(errorMessage);

            RedirectToPage("./Create");
        }
        else 
        {
            var infoMessage = $"{_className}: Display Page";
            _logger.LogInformation(infoMessage);
        }
    }
    #endregion
}
#endregion