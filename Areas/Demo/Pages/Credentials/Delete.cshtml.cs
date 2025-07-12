#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

#region DeleteModel
/// <summary>
/// Represents a Razor Page model for handling the deletion of credentials.
/// </summary>
/// <remarks>This class provides methods to handle GET and POST requests for deleting credentials. It interacts
/// with the database context to retrieve and delete credentials and logs relevant information during the process. The
/// class requires a valid <see cref="AppDbContext"/> and <see cref="ILogger{TCategoryName}"/>  to function
/// correctly.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class DeleteModel(AppDbContext context, ILogger<DeleteModel> logger) : PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<DeleteModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the credential to be deleted.
    /// </summary>
    [BindProperty]
    public Credential credential { get; set; } = default!;
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request to retrieve the credential for deletion.
    /// </summary>
    /// <param name="id">The ID of the credential to retrieve.</param>
    /// <returns>The page result or a not found result.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            var errorMessage = $@"Delete: id is null {id}.";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(id));
        }

        var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if (credential is null)
        {
            var errorMessage = $"Delete: there are no credentials to delete!";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(credential));
        }
        else
        {
            var infoMessage = $"Delete: credentials to delete {credential}!";
            _logger.LogInformation(infoMessage);
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
        if (id is null)
        {
            var Message = $"Credentials/Delete - id is null";
            _logger.LogError(Message);

            throw new NullReferenceException(nameof(id));
        }

        var credential = await _context.Credentials.FindAsync(id);
        if (credential is null)
        {
            var Message = $"Delete: credential is null";
            _logger.LogError(Message);

            throw new NullReferenceException(nameof(credential)); 
        }
        else
        {
            _context.Credentials.Remove(credential);
            await _context.SaveChangesAsync();

            var infoMessage = $"Delete: credential {credential} removed from local store";
            _logger.LogInformation(infoMessage);
        }

        return RedirectToPage("./Index");
    }
    #endregion
}
#endregion