#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Pages
{
    public class TermsModel(ILogger<TermsModel> logger) : PageModel
    {
        private readonly ILogger<TermsModel> _logger = logger;   
        public void OnGet()
        {
            _logger.LogInformation("Terms page loaded.");
        }
    }
}
