#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

//[Authorize]
public class IndexModel(AppDbContext context,ILogger<IndexModel> logger):PageModel
{
    #region ctor

    public readonly ILogger<IndexModel> _logger = logger;
    public readonly AppDbContext _context = context;

    #endregion

    [BindProperty]
    public string SiteUrl { get; set; } = default;
    public Site_Credential SiteCredential { get; set; }// site object to hold credentials for Acumatica ERP connection


    public IActionResult OnGet()
    {
        // Get the selected connection credentials to access Acumatica ERP
        Site_Credential SiteCredential = new Site_Credential(_context,_logger);

        Credential credential = SiteCredential.GetSiteCredential().Result;

        if(credential is null)
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
}