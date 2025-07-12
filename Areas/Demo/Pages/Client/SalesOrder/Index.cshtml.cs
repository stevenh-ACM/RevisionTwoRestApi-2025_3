#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

[Authorize]

#region IndexModel
/// <summary>
/// Represents the model for editing sales orders in the application.
/// </summary>
/// <remarks>The <see cref="IndexModel"/> class provides functionality for handling HTTP GET and POST requests 
/// related to editing sales orders. It includes properties for binding sales order data, managing  selectable options,
/// and tracking parameters for filtering and processing sales orders.  This model is designed to interact with the
/// application's database context and logging system.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param> 
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger) : PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    private readonly ILogger<IndexModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the list of parameters associated with the operation.
    /// </summary>
    public List<object> Parms { get; set; } = [ ];
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-90); //default to 90 days ago
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ToDate { get; set; } = DateTime.Now; //default to today
    [BindProperty]
    public int NumRecords { get; set; } = 10; //default
    public string Selected_SalesOrder_Type { get; set; } = "SO"; // Sales Order Type is Sales Order default

    /// <summary>
    /// Gets or sets the list of selectable sales order types.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; set; } = new Combo_Boxes().ComboBox_SalesOrder_Types;
    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests for the Index page.
    /// </summary>
    public void OnGet()
    {
        var infoMessage = $"Index: OnGet";
        _logger.LogInformation(infoMessage);
    }

    /// <summary>
    /// Handles POST requests for the Index page.
    /// </summary>
    /// <returns>A redirection to the Details page.</returns>
    public IActionResult OnPost()
    {
        //SetParameters();

        var infoMessage = $"Index: OnPost";
        _logger.LogInformation( infoMessage);

        TempData[ "EditFlag" ] = false;
        TempData[ "DeleteFlag" ] = false;
        TempData[ "RefreshFlag" ] = true;

        return RedirectToPage("./Details");
    }
    #endregion 
}
#endregion
