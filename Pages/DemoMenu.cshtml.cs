//#nullable disable

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;

//using RevisionTwoApp.RestApi.Auxiliary;
//using RevisionTwoApp.RestApi.Data;
//using RevisionTwoApp.RestApi.Models;

//namespace RevisionTwoApp.RestApi.Pages;

//public class DemoMenuModel : PageModel
//{
//    #region ctor
//    private readonly ILogger<DemoMenuModel> _logger;
//    private readonly AppDbContext _context;

//    public DemoMenuModel(AppDbContext context,ILogger<DemoMenuModel> logger)
//    {
//        _context = context;
//        _logger = logger;
//    }
//    #endregion

//    public List<Credential> Credentials { get; set; }
   
//    [BindProperty]
//    public string SiteUrl { get; set; }

//    public async Task<IActionResult> OnGetAsync()
//    {
//        Credentials = await _context.Credentials.FirstOrDefault();

//        if(Credentials is null)
//        {
//            _logger.LogError($"No Credentials exist. Please create at least one Credential!");
//            return RedirectToPage("Pages/Home/Index");
//        }
//        else
//        {
//            //Get the selected credentials to access Acumatica ERP with
//            var id = new AuthId().getAuthId(Credentials) - 1;
//            _logger.LogInformation($"Credential secured. SiteURL is {Credentials[id].SiteUrl} and id is {id}");

//            SiteUrl = Credentials[id].SiteUrl;
//        }
//        return Page();
//    }
//}
