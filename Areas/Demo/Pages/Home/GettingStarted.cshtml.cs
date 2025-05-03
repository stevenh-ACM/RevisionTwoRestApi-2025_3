    #nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

/// <summary>
/// Represents the model for the "Getting Started" page.
/// </summary>
public class GettingStartedModel:PageModel
{
    private readonly ILogger<GettingStartedModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GettingStartedModel"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to log information.</param>
    public GettingStartedModel(ILogger<GettingStartedModel> logger) => _logger = logger;

    /// <summary>
    /// Handles GET requests for the "Getting Started" page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Home/Get Started Page");
    }
}
