
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Helper;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

public class CreateModel(AppDbContext context, ILogger<CreateModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<CreateModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region properties

    [BindProperty]
    public SalesOrder_App salesOrder_App { get; set; } = default!;

    public List<SelectListItem> Selected_SalesOrder_Types { get; } = new Combo_Boxes().ComboBox_SalesOrder_Types;
    public List<SelectListItem> Selected_SalesOrder_Statuses { get; } = new Combo_Boxes().ComboBox_SalesOrder_Statuses;

    [BindProperty]
    public Combo_Boxes combo_Boxes { get; set; } = new();

    [BindProperty]
    public DateTime lastModified { get; set; } = DateTime.Now;

    #endregion

    #region methods
    public IActionResult OnGet()
    {
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid || _context.SalesOrders == null || salesOrder_App == null)
        {
            return Page();
        }

        _context.SalesOrders.Add(salesOrder_App);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    #endregion
}