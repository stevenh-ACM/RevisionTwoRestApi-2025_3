#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Helper;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

public class IndexModel(AppDbContext context,ILogger<IndexModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<IndexModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region Properties
    /// <summary>
    /// Search fields
    /// </summary>
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-180); //default to 180 days ago

    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ToDate { get; set; } = DateTime.Now; //default to today

    [BindProperty]
    public int NumRecords { get; set; } = 10; //default 
    [BindProperty]
    public string Selected_SalesOrder_Type { get; set; } = "SO"; //Sales Order Type is Sales Order default
    public List<SelectListItem> Selected_SalesOrder_Types { get; set; } = new Combo_Boxes().ComboBox_SalesOrder_Types;

    public List<object> Parms { get; set; } = [];
    public string Message { get; private set; }
    #endregion

    #region methods
    public void OnGet()
    {
        Message = $"Main: OnGet";
        _logger.LogInformation(message: Message);
    }

    public IActionResult OnPost()
    {
        SetParms();
        Message = $"Main: OnPost";
        _logger.LogInformation(message: Message);

        return RedirectToPage("./Details");
    }

    #endregion

    #region parameters

    private void SetParms() 
    {
        Parms = [ FromDate,
                                  ToDate,
                                  NumRecords,
                                  Selected_SalesOrder_Type ];
        if(Parms is null)
        {
            Message = $"Main: No parameters exist. Please check your parameters!";
            _logger.LogError(message: Message);
            throw new NullReferenceException(nameof(Parms));
        }

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["EditFlag"] = false;
        TempData["DeleteFlag"] = false;

        Message = $"Main: Date Range is {FromDate} to {ToDate}. Number of Records is {NumRecords} and the SalesOrder Type is {Selected_SalesOrder_Type}";
        _logger.LogInformation(message: Message);
        
        return;
    }

    #endregion
}
