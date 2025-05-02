#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

//[Authorize]
public class IndexModel:PageModel
{
    #region ctor
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context,ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }
    #endregion

    public List<Credential> Credentials { get; set; }

    [BindProperty]
    public string SiteUrl { get; set; } = default;

    public async Task<IActionResult> OnGetAsync()
    {
        Credentials = await _context.Credentials.ToListAsync();

        if(Credentials is null)
        {
            _logger.LogError($"No Credentials exist. Please create at least one Credential!");
            return RedirectToPage("Pages/Home/Index");
        }
        else
        {
            //Get the selected credentials to access Acumatica ERP with
            var id = new AuthId().getAuthId(Credentials) - 1;
            _logger.LogInformation($"Credential secured. SiteURL is {Credentials[id].SiteUrl} and id is {id}");

            SiteUrl = Credentials[id].SiteUrl;

            _logger.LogInformation("Home/Index Page");
        }
        return Page();
    }
}