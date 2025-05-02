
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

public class BackgroundModel:PageModel
{
    private readonly ILogger<BackgroundModel> _logger;

    public BackgroundModel(ILogger<BackgroundModel> logger) => _logger = logger;
    public void OnGet()
    {
        _logger.LogInformation("Home/Background Page");
    }
}
