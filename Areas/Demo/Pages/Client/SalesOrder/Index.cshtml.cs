#nullable disable

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Helper;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1587 // XML comment is not placed on a valid language element
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

/// <summary>
/// Index a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
#region IndexModel

[Authorize]
/// <summary>
/// Represents the Index page model for the SalesOrder in the Demo area.
/// </summary>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
{
    #region ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    /// <param name="logger">The logger used for logging information and errors.</param>
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region Properties
    /// <summary>
    /// Search fields
    /// </summary>
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-180); //default to 180 days ago

    /// <summary>
    /// Gets or sets the end date for the search range. Defaults to today.
    /// </summary>
    public DateTime ToDate { get; set; } = DateTime.Now; //default to today

    /// <summary>
    /// Gets or sets the number of records to retrieve. Defaults to 10.
    /// </summary>
    [BindProperty]
    public int NumRecords { get; set; } = 10; //default 
    /// <summary>
    /// Gets or sets the selected sales order type. Defaults to "SO".
    /// </summary>
    public string Selected_SalesOrder_Type { get; set; } = "SO"; // Sales Order Type is Sales Order default
    /// <summary>
    /// Gets or sets the list of selectable sales order types.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; set; } = new Combo_Boxes().ComboBox_SalesOrder_Types;

    /// <summary>
    /// Gets or sets the parameters for the SalesOrder.
    /// </summary>
    public List<object> Parms { get; set; } = [];
    /// <summary>
    /// Gets or sets the message for the Index page.
    /// </summary>
    public string Message { get; private set; }
    #endregion

    #region methods

    /// <summary>
    /// Handles GET requests for the Index page.
    /// </summary>
    public void OnGet()
    {
        Message = $"Main: OnGet";
        _logger.LogInformation(message: Message);
    }

    /// <summary>
    /// Handles POST requests for the Index page.
    /// </summary>
    /// <returns>A redirection to the Details page.</returns>
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
#endregion
