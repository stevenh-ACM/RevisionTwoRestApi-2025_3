#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Credential = RevisionTwoApp.RestApi.Models.Credential;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

#region CreateModel
/// <summary>
/// Represents the page model for creating a new credential.
/// </summary>
/// <remarks>This class provides functionality for handling HTTP GET and POST requests related to the creation of
/// credentials. It uses an <see cref="AppDbContext"/> for database operations and an <see
/// cref="ILogger{TCategoryName}"/> for logging.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class CreateModel(AppDbContext context, ILogger<CreateModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<CreateModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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