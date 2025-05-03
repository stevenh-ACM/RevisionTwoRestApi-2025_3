#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region IndexModel
/// <summary>
/// Represents the model for the Index page in the Home area of the Demo section.
/// </summary>
public class IndexModel(AppDbContext context,ILogger<IndexModel> logger):PageModel
{
    #region ctor

    /// <summary>
    /// Logger instance for logging information and errors.
    /// </summary>
    public readonly ILogger<IndexModel> _logger = logger;

    /// <summary>
    /// Database context for accessing application data.
    /// </summary>
    public readonly AppDbContext _context = context;

    #endregion

    #region properties

    /// <summary>
    /// The URL of the site.
    /// </summary>
    [BindProperty]
    public string SiteUrl { get; set; } = default;

    /// <summary>
    /// Object to hold credentials for Acumatica ERP connection.
    /// </summary>
    public Site_Credential SiteCredential { get; set; }

    #endregion

    #region methods

    /// <summary>
    /// Handles GET requests to the Index page.
    /// </summary>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    public IActionResult OnGet()
    {
        // Get the selected connection credentials to access Acumatica ERP
        Site_Credential SiteCredential = new Site_Credential(_context,_logger);

        Credential credential = SiteCredential.GetSiteCredential().Result;

        if (credential is null)
        {
            _logger.LogError($"No Credentials exist. Please create at least one Credential!");
            return RedirectToPage("Pages/Home/Index");
        }
        else
        {
            _logger.LogInformation($"Credential secured. SiteURL is {credential.SiteUrl}");

            SiteUrl = credential.SiteUrl;

            _logger.LogInformation("Home/Index Page");
        }
        return Page();
    }
    #endregion
}
#endregion