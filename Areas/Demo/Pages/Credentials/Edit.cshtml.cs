#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type
// or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language
// element

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

#region EditModel
/// <summary>
/// Represents the page model for editing credentials.
/// </summary>
/// <remarks>This class provides functionality to handle GET and POST requests
/// for editing credentials. It interacts with the database context to retrieve
/// and update credential data.</remarks> <param name="context"></param> <param
/// name="logger"></param>
[BindProperties]
public class EditModel(AppDbContext context, ILogger<EditModel> logger)
    : PageModel {
#region ctor
  /// <summary>
  /// Initializes a new instance of the class with the specified database
  /// context.
  /// </summary>
  private readonly AppDbContext _context =
      context ?? throw new ArgumentNullException(nameof(context));
  private readonly ILogger<EditModel> _logger =
      logger ?? throw new ArgumentNullException(nameof(logger));
  private readonly string _className = nameof(EditModel);
#endregion ctor

#region properties
  /// <summary>
  /// Gets or sets the credential being edited.
  /// </summary>
  public Credential credential { get; set; } = default!;
  /// <summary>
  /// id passed in the query string to identify the credential to edit.
  /// </summary>
  public int? id { get; set; }

#endregion

#region methods
  /// <summary>
  /// Handles the GET request to retrieve the credential for editing.
  /// </summary>
  /// <param name="id">The ID of the credential to edit.</param>
  /// <returns>The page result or a not found result.</returns>
  public async Task<IActionResult> OnGetAsync(int? id) {
    if (id is null) {
      var errorMessage = $"{_className}:: No ID provided, {id} was passed in.";
      _logger.LogError(errorMessage);

      throw new ArgumentNullException(nameof(id));
    }

    credential =
        await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
    if (credential is null) {
      var errorMessage = $"{_className}: No credential with the Id {id} found.";
      _logger.LogError(errorMessage);

      return NotFound();
    }

    return Page();
  }

  /// <summary>
  /// Handles the POST request to update the credential.
  /// </summary>
  /// <returns>A redirect to the index page or the current page if validation
  /// fails.</returns>
  public async Task<IActionResult> OnPostAsync() {
    credential.Id = id ?? throw new ArgumentNullException(nameof(id));
    if (!ModelState.IsValid) {
      var Errors = ModelState.Values.SelectMany(v => v.Errors);

      var errorMessage =
          $@"{_className}: Model State is inValid for Id {id} ModelState Values are:{Errors}";
      _logger.LogError(errorMessage);

      return Page();
    }
    _context.Attach(credential).State = EntityState.Modified;
    _ = await _context.SaveChangesAsync();

    var infoMessage =
        $@"{_className}: Credential {credential} updated successfully!";
    _logger.LogInformation(infoMessage);

    return RedirectToPage("./Index");
  }
#endregion
}
#endregion
