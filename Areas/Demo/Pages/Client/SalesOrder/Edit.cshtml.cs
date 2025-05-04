#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Helper;
using RevisionTwoApp.RestApi.Models.App;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

/// <summary>
/// Edit a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class EditModel(AppDbContext context,ILogger<EditModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<EditModel> _logger = logger;
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Gets or sets the data associated with the EditModel.
    /// </summary>
    public string Data { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the parameters associated with the EditModel.
    /// </summary>
    public List<object> Parms { get; set; } = [new()];

    /// <summary>
    /// Gets or sets the starting date for the sales order.
    /// </summary>
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Gets or sets the ending date for the sales order.
    /// </summary>
    public DateTime ToDate { get; set; }

    /// <summary>
    /// Gets or sets the number of records associated with the sales order.
    /// </summary>
    public int NumRecords { get; set; }

    /// <summary>
    /// Gets or sets the selected sales order type.
    /// </summary>
    public string Selected_SalesOrder_Type { get; set; }

    [BindProperty]
    public SalesOrder_App salesOrder_App { get; set; }

    /// <summary>
    /// Gets the list of selectable sales order types.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; } = new Combo_Boxes().ComboBox_SalesOrder_Types;

    /// <summary>
    /// Gets the list of selectable sales order statuses.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Statuses { get; } = new Combo_Boxes().ComboBox_SalesOrder_Statuses;

    /// <summary>
    /// Gets or sets the message associated with the EditModel.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Gets or sets the sales order application data.
    /// </summary>
    [BindProperty]
    public SalesOrder_App SalesOrderApp { get; set; }

    /// <summary>
    /// Handles the GET request for editing a sales order.
    /// </summary>
    /// <param name="id">The ID of the sales order to edit.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        GetParms();

        if (id == null || _context.SalesOrders == null)
        {
            Message = $"Edit: Id {id} or SalesOrder context {_context.ContextId} is null";
            _logger.LogError(message: Message);
            return NotFound();
        }
        SalesOrder_App salesOrder_app = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);

        if (salesOrder_app == null)
        {
            Message = $"Edit: Edit of id {id} failed";
            _logger.LogError(message: Message);
            return NotFound();
        }
        else
        {
            salesOrder_App = salesOrder_app;
        }
        salesOrder_App.Status = Selected_SalesOrder_Statuses.FirstOrDefault(x => x.Text == salesOrder_App.Status)?.Value ?? string.Empty;
        salesOrder_App.LastModified = DateTime.Now;
        SetParms();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        GetParms();
        if(!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            _logger.LogError($"Edit: Model State is inValid for Id {id} or SalesOrder context {_context.ContextId}");
            _logger.LogError($"Edit: ModelState Values are:{errors}");
            return Page();
        }
        _context.Attach(salesOrder_App).State = EntityState.Modified;
        var status = salesOrder_App.Status;

        Message = $"Edit: salesOrder_App.Status is {status}";
        _logger.LogInformation(message: Message);

        salesOrder_App.Status = Selected_SalesOrder_Statuses.FirstOrDefault(x => x.Value == salesOrder_App.Status)?.Text ?? string.Empty;
        var status1 = salesOrder_App.Status;

        Message = $"Edit: salesOrder_App.Status is {status1}";
        _logger.LogInformation(message: Message);

        try
        {
            await _context.SaveChangesAsync(); //Save Modified Record to cache
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!SalesOrder_AppExists(salesOrder_App.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        SetParms();
        return RedirectToPage("./Details");
    }

    private bool SalesOrder_AppExists(int id)
    {
        return (_context.SalesOrders?.Any(e => e.Id == id)).GetValueOrDefault();
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
        TempData["EditFlag"] = true;

        Message = $"Edit: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(message: Message);

        return;
    }

    #endregion
}