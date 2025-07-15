#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting.Hosting;

using System.ComponentModel.DataAnnotations;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

[Authorize]

#region IndexModel
[BindProperties]
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
    private readonly string _className = nameof(IndexModel);
    #endregion

    #region properties
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime FromDate { get; set; } = (DateTime)(Globals.GetGlobalProperty("FromDate", logger) ?? DateTime.Now.AddDays(-30));

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime ToDate { get; set; } = (DateTime)(Globals.GetGlobalProperty("ToDate", logger) ?? DateTime.Now); 

    public int NumRecords { get; set; } = (int)Globals.GetGlobalProperty("NumRecords", logger);

    public string Selected_SalesOrder_Type { get; set; } = Globals.GetGlobalProperty("Selected_SalesOrder_Type", logger)?.ToString() ?? "SO";

    /// <summary>
    /// Gets or sets the list of selectable sales order types.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; set; } = new Combo_Boxes().ComboBox_SalesOrder_Types;
    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests for the Index page.
    /// </summary>
    public void OnGet( )
    {
        Globals.LogGlobalProperties(_logger);
    }

    /// <summary>
    /// Handles POST requests for the Index page.
    /// </summary>
    /// <returns>A redirection to the Details page.</returns>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = $"{_className}: ModelState is invalid.";
            _logger.LogError(errorMessage);

            return Page();
        }

        Globals.SetGlobalProperty("FromDate", FromDate, _logger);
        Globals.SetGlobalProperty("ToDate", ToDate, _logger);
        Globals.SetGlobalProperty("NumRecords", NumRecords, _logger);
        Globals.SetGlobalProperty("Selected_SalesOrder_Type", Selected_SalesOrder_Type, _logger);

        Globals.LogGlobalProperties(_logger);

        return RedirectToPage("./Details");
    }
    #endregion 
}
#endregion
