#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Pages;
/// <summary>
/// Represents the model for the Privacy page in the application.
/// </summary>
/// <remarks>This class is used to handle requests and responses for the Privacy page. It logs an informational
/// message when the page is accessed.</remarks>
public class PrivacyModel(ILogger<PrivacyModel> logger):PageModel
{
    private readonly ILogger<PrivacyModel> _logger = logger;

    /// <summary>
    /// Handles GET requests for the Privacy page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation($@"Privacy page loaded.");
    }
}