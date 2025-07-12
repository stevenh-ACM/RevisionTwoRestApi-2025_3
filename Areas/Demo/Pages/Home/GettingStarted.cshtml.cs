#nullable disable

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region GettingStartedModel
/// <summary>
/// Represents the model for the "Getting Started" page, providing functionality to handle GET requests.
/// </summary>
/// <remarks>This class is used in Razor Pages to manage the "Getting Started" page. It logs information about
/// page access and can be extended to include additional functionality for the page.</remarks>
/// <param name="logger"></param>
public class GettingStartedModel(ILogger<GettingStartedModel> logger) : PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly ILogger<GettingStartedModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    #endregion ctor

    #region methods
    /// <summary>
    /// Handles GET requests for the "Getting Started" page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Home/Get Started Page");
    }
    #endregion
}
#endregion