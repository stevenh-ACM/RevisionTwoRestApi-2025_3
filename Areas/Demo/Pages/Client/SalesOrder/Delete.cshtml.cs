#nullable disable

using Acumatica.RESTClient.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models.App;
using RevisionTwoApp.RestApi.Models;

using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.Default_24_200_001.Model;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

    /// <summary>
    /// Represents a SalesOrder page entity to be deleted.
    /// </summary>
    [BindProperty]
    public SalesOrder_App salesOrder { get; set; } = default!;

    /// <summary>
    /// Represents a SalesOrder entity to be deleted.
    /// </summary>
    public SalesOrder_App SalesOrder { get; set; } = default!;

    public List<SalesOrder_App> SalesOrders { get; set; } = default!;

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

        // retrieve the sales order from the database
        salesOrder = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);
        if (salesOrder == null)
        {
            Message = $"Delete: deletion of id {id} failed";
            _logger.LogError(message: Message);
            return NotFound();
        }
        else 
        {
            _logger.LogInformation($@"salesOrder is {salesOrder}");
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

        SalesOrder = await _context.SalesOrders.FindAsync(id);

        if (SalesOrder == null)
        {
            _logger.LogError("Delete-OnGetAsync: No Sales Orders exist or Model is inValid.");
            return Page();
        }

        // remove salesOrder from SalesOrder_App cache
        _context.Remove(SalesOrder);
        _context.SaveChanges();

        // get current Acumatcia ERP credentials to login
        Site_Credential SiteCredential = new(_context, _logger);

        Credential credential = SiteCredential.GetSiteCredential().Result;

        if (credential == null)
        {
            _logger.LogError("Delete-OnPostAsync: No credentials found. Please create at least one credential.");
            return Page();
        }

        var client = new ApiClient(credential.SiteUrl,
                                   requestInterceptor: RequestLogger.LogRequest,
                                   responseInterceptor: RequestLogger.LogResponse,
                                   ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                   );

        if (client.RequestInterceptor is null)
        {
            var Message = $"Delete: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(client));
        }
        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName, credential.Password, "", "", "");
            if (client.RequestInterceptor is null)
            {
                var Message = $"Delete: Failure to create a context for client login: UserName of " +
                              $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(Message);
                throw new NullReferenceException(nameof(client));
            }
            else
            {
                IEnumerable<string> Ids = [ ];
                var OrderNbr = SalesOrder.OrderNbr;
                var OrderType = SalesOrder.OrderType;
                var Status = SalesOrder.Status;
                var StatusList = new List<string> { "Open", "On Hold", "Rejected", "Expired" };

                _logger.LogInformation($"Delete: Deleting Sales Order {OrderType} {OrderNbr}.");

                var so = client.GetByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(ids: Ids, select: "OrderType, OrderNbr, Status");
                if (StatusList.Contains(Status)) 
                {
                    if (Ids == null || Ids.Any())
                    {
                        _logger.LogError($@"Sales Order {OrderType} {OrderNbr} is not in a valid status for deletion. Status is {Status}.");
                        return Page();
                    }
                    else
                    {
                        _logger.LogInformation($@"Sales Order {OrderType} {OrderNbr} is in a valid status for deletion. Status is {Status}.");
                    }
                }
                else
                {
                    _logger.LogError($@"Sales Order {OrderType} {OrderNbr} is not in a valid status for deletion. Status is {Status}.");
                    return Page();
                }
                var entityish = client.ResponseInterceptor;
                var result = client.DeleteByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(Ids);

                //var result = client.DeleteAsync(new Acumatica.Default_24_200_001.Model.SalesOrder
                //                                    {
                //                                        OrderType = OrderType,
                //                                        OrderNbr = OrderNbr
                //                                    });
                ;
                if (result == null)
                {
                    _logger.LogError($"Delete: Failed to delete Sales Order {OrderNbr}.");
                    throw new NullReferenceException(nameof(result));
                }
                else
                {
                    _logger.LogInformation(@$"Delete: Sales Order {OrderNbr} deleted.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create-OnPostAsync: Error creating Sales Order.");
            return Page();
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                var Message = $"Details: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(message: Message);
            }
            else
            {
                var Message = $"Details: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(Message);
            }

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

