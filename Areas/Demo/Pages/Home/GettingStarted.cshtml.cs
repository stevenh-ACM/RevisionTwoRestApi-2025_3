#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

public class GettingStartedModel:PageModel
{
    private readonly ILogger<GettingStartedModel> _logger;

    public GettingStartedModel(ILogger<GettingStartedModel> logger) => _logger = logger;

    public void OnGet()
    {
        _logger.LogInformation("Home/Get Started Page");
    }
}
