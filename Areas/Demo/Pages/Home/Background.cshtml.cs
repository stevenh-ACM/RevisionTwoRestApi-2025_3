
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region BackgroundModel
/// <summary>
/// Represents the model for the Background page, providing functionality to handle HTTP GET requests.
/// </summary>
/// <remarks>This class is used in ASP.NET Core Razor Pages to manage the Background page. It logs information
/// about page access using the provided <see cref="ILogger{BackgroundModel}"/> instance.</remarks>
/// <param name="logger"></param>
public class BackgroundModel(ILogger<BackgroundModel> logger): PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly ILogger<BackgroundModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _className = nameof(BackgroundModel);
    #endregion ctor

    #region methods
    /// <summary>
    /// Handles GET requests to the Background page.
    /// </summary>
    public void OnGet()
    {
        var infoMessage = $"{_className} - OnGet()";
        _logger.LogInformation(infoMessage);
    }
    #endregion
}
#endregion