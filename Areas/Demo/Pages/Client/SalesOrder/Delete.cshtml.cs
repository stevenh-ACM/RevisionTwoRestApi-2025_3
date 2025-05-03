#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

/// <summary>
/// Delete a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class DeleteModel(AppDbContext context,ILogger<IndexModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<IndexModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region properties

    [BindProperty]
    public SalesOrder_App salesOrder_App { get; set; } = default!;

    /// <summary>
    /// Gets or sets the message for the current operation.
    /// </summary>
    public string Message { get; private set; }
    /// <summary>
    /// Gets or sets the parameters used for processing or filtering.
    /// </summary>
    public List<object> Parms { get; private set; }

    /// <summary>
    /// Gets or sets the start date for filtering or processing.
    /// </summary>
    public DateTime FromDate { get; private set; }

    /// <summary>
    /// Gets or sets the end date for filtering or processing.
    /// </summary>
    public DateTime ToDate { get; private set; }

    /// <summary>
    /// Gets or sets the number of records.
    /// </summary>
    public int NumRecords { get; private set; }

    /// <summary>
    /// Gets the selected sales order type.
    /// </summary>
    public string Selected_SalesOrder_Type { get; private set; }

    /// <summary>
    /// Represents a SalesOrder entity to be deleted.
    /// </summary>
    [BindProperty]
    public SalesOrder_App SalesOrderApp { get; set; } = default!;

    /// <summary>
    /// Handles the GET request to retrieve a SalesOrder by its ID for deletion.
    /// </summary>
    /// <param name="id">The ID of the SalesOrder to retrieve.</param>
    /// <returns>An IActionResult that renders the page if successful, or NotFound if the ID is invalid.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.SalesOrders == null)
        {
            Message = $"Delete: Id {id} or SalesOrder context {_context.ContextId} is null";
            _logger.LogError(message: Message);
            return NotFound();
        }

        var salesOrder_app = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);

        if (salesOrder_app == null)
        {
            Message = $"Delete: deletion of id {id} failed";
            _logger.LogError(message: Message);
            return NotFound();
        }
        else
        {
            salesOrder_App = salesOrder_app;
        }
        return Page();
    }

    /// <summary>
    /// Handles the POST request to delete a SalesOrder by its ID.
    /// </summary>
    /// <param name="id">The ID of the SalesOrder to delete.</param>
    /// <returns>An IActionResult that redirects to the Details page if successful, or NotFound if the ID is invalid.</returns>
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        GetParms();
        if (id == null || _context.SalesOrders == null)
        {
            Message = $"Delete: Id {id} or SalesOrder context {_context.ContextId} is null";
            _logger.LogError(message: Message);

            return NotFound();
        }
        var salesOrder_app = await _context.SalesOrders.FindAsync(id);

        if (salesOrder_app != null)
        {
            salesOrder_App = salesOrder_app;
            _context.SalesOrders.Remove(salesOrder_App);

            await _context.SaveChangesAsync();
        }
        SetParms();

        return RedirectToPage("./Details");
    }
    #endregion

    #region parameters

    private void GetParms()
    {
        Parms = JsonConvert.DeserializeObject<List<object>>((string)TempData["parms"]);
        if(Parms is null)
        {
            Message = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(message: Message);
            throw new NullReferenceException(nameof(Parms));
        }

        FromDate = (DateTime)Parms[0];
        ToDate = (DateTime)Parms[1];
        NumRecords = Convert.ToInt32(Parms[2]);
        Selected_SalesOrder_Type = (string)Parms[3];

        Message = $"Details: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(message: Message);

        return;
    }

    private void SetParms()
    {
        Parms = [ FromDate,
                             ToDate,
                             NumRecords,
                             Selected_SalesOrder_Type ];
        if(Parms is null)
        {
            _logger.LogError($"Details: No parameters exist. Please check your parameters!");
            throw new NullReferenceException(nameof(Parms));
        }

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["DeleteFlag"] = true;

        Message = $"Edit: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(message: Message);

        return;
    }
    #endregion
}

