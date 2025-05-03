#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Pages;
/// <summary>
/// Represents the model for the Index page in an ASP.NET Core Razor Pages application. 
/// </summary>
/// <remarks>This class is responsible for handling the logic and data associated with the Index page. It includes
/// a method to handle GET requests and logs when the page is loaded.</remarks>
public class IndexModel(ILogger<IndexModel> logger):PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    /// <summary>
    /// Handles GET requests for the Index page.
    /// </summary>
    /// <remarks>This method is invoked when the Index page is accessed via a GET request. It logs a message indicating that the page has been loaded.</remarks>
    public void OnGet()
    {
        _logger.LogInformation("Index page loaded.");
    }
}

