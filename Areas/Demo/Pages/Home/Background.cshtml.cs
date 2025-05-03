
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

/// <summary>
/// Represents the model for the Background page in the Home area.
/// </summary>
public class BackgroundModel:PageModel
{
    private readonly ILogger<BackgroundModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BackgroundModel"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging information.</param>
    public BackgroundModel(ILogger<BackgroundModel> logger) => _logger = logger;

    /// <summary>
    /// Handles GET requests to the Background page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Home/Background Page");
    }
}
