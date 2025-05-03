#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Pages;
/// <summary>
/// Represents the model for the Terms page in an ASP.NET Core Razor Pages application.
/// </summary>
/// <remarks>This class is responsible for handling the GET requests to the Terms page. It logs an informational
/// message when the page is accessed.</remarks>
public class TermsModel(ILogger<TermsModel> logger):PageModel
{
    private readonly ILogger<TermsModel> _logger = logger;

    /// <summary>
    /// Handles GET requests to the Terms page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Terms page loaded.");
    }
}