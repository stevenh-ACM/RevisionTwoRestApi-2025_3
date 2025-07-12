#nullable enable

using Activity = System.Diagnostics.Activity;

namespace RevisionTwoApp.RestApi.Pages;
/// <summary>
/// Represents the model for handling error pages in the application.
/// </summary>
/// <remarks>This model is used to display error information, including the request ID, on error pages. It also
/// logs error details for diagnostic purposes.</remarks>
/// <param name="logger"></param>
[ResponseCache(Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel(ILogger<ErrorModel> logger) : PageModel
{    
    private readonly ILogger<ErrorModel> _logger = logger;
    /// <summary>
    /// Gets or sets the unique identifier for the current request.
    /// </summary>
    public string? RequestId { get; set; }
    /// <summary>
    /// Indicates whether the current request ID should be displayed.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>
    /// Handles GET requests for the error page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation($@"Error: Request ID is {RequestId}.");
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}

