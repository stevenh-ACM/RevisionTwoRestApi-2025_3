#nullable disable

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

#region DetailsModel
/// <summary>
/// Represents the model for handling credential details in a Razor Page.
/// </summary>
/// <remarks>The <see cref="DetailsModel"/> class is used to manage the retrieval and display of credential
/// details. It provides functionality to handle GET requests for fetching credential information by ID.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class DetailsModel(AppDbContext context, ILogger<DetailsModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<DetailsModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _className = nameof(DetailsModel);
    #endregion ctor

    #region properties
    /// <summary>
    /// Gets or sets the credential details.
    /// </summary>
    public Credential credential { get; set; } = default!;
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request to retrieve credential details by ID.
    /// </summary>
    /// <param name="id">The ID of the credential.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            var errorMessage = $"{_className}: id is null {id}.";
            _logger.LogError(errorMessage);

            throw new ArgumentNullException(nameof(id));
        }

        var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if (credential is null)
        {
            var errorMessage = $"{_className}: there are no credentials with id {id} to display!";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        return Page();
    }
    #endregion
}
#endregion