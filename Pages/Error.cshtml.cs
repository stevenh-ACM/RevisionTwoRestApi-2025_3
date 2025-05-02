#nullable enable

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Pages;

[ResponseCache(Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel(ILogger<ErrorModel> logger) : PageModel
{    
    private readonly ILogger<ErrorModel> _logger = logger;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public void OnGet() 
    {
        _logger.LogInformation($@"Error: Request ID is {RequestId}.");
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}

