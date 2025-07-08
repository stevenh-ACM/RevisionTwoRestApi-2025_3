#nullable disable

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Helper;
using RevisionTwoApp.RestApi.Models.App;

using Credential = RevisionTwoApp.RestApi.Models.Credential;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region EditModel
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
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the sales order application model used for binding and processing sales order data.
    /// </summary>
    [BindProperty]
    public SalesOrder_App salesOrder { get; set; }

    Site_Credential SiteCredential { get; set; }

    Credential credential { get; set; }

    /// <summary>
    /// Gets or sets the parameters associated with the EditModel.
    /// </summary>
    public List<object> Parms { get; set; } = [new()];
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int NumRecords { get; set; }
    public string Selected_SalesOrder_Type { get; set; }

    /// <summary>
    /// Gets the list of selectable sales order types.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; } = new Combo_Boxes().ComboBox_SalesOrder_Types;

    /// <summary>
    /// Gets the list of selectable sales order statuses.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Statuses { get; } = new Combo_Boxes().ComboBox_SalesOrder_Statuses;
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request for editing a sales order.
    /// </summary>
    /// <param name="id">The ID of the sales order to edit.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        GetParms();

        if (id is null)
        {
            var errorMessage = $"Edit: Id {id}";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        //Retrieve the salesOrder from the local store using id
        salesOrder = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);
        if (salesOrder is null)
        {
            var errorMessage = $"Edit: Edit of id {id} failed";
            _logger.LogError(errorMessage);

            return NotFound();
        }
        else
        {
            salesOrder.LastModified = DateTime.Now;
            salesOrder.ShipmentDate = DateTime.Now.AddDays(1);
        }

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to update a sales order.
    /// </summary>
    /// <remarks>This method validates the model state, updates the sales order's status, and saves the changes to the
    /// database. If a concurrency conflict occurs during the update, the method checks whether the sales order still exists
    /// and throws an exception if the conflict cannot be resolved.</remarks>
    /// <param name="id">The identifier of the sales order to update. Must be a valid sales order ID.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns the current page if the model state
    /// is invalid, a "Not Found" result if the sales order does not exist,  or redirects to the details page upon
    /// successful update.</returns>
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            var errorMessage = $@"Edit: Model State is inValid for Id {id} ModelState Values are:{errors}";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        _context.Attach(salesOrder).State = EntityState.Modified;
        await _context.SaveChangesAsync(); //Save Modified Record to cache

        var infoMessage = $"Edit: Edited salesOrder is {salesOrder}";
        _logger.LogInformation(infoMessage);

        //salesOrder.Status = Selected_SalesOrder_Statuses.FirstOrDefault(x => x.Value == salesOrder.Status)?.Text ?? string.Empty;

        // get current Acumatica ERP credentials to login
        SiteCredential = new(_context, _logger);

        credential = SiteCredential.GetSiteCredential().Result;
        if (credential is null)
        {
            var errorMessage = $"Edit: No credentials found. Please create at least one credential.";
            _logger.LogError(errorMessage);

            return RedirectToPage("Demo/Credentials");
        }

        var client = new ApiClient(
                                    credential.SiteUrl,
                                            requestInterceptor: RequestLogger.LogRequest,
                                            responseInterceptor: RequestLogger.LogResponse,
                                            ignoreSslErrors: true); // this is here to allow testing with self-signed certificates
        if (client.RequestInterceptor is null)
        {
            var errorMessage = $"Edit: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(client));
        }

        // attempt to delete the selected Sales Order from Acumatica ERP and ensure logout
        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName, credential.Password, "", "", "");
            if (client.RequestInterceptor is null)
            {
                var errorMessage = $"Edit: Failure to create a context for client login: UserName of " +
                                         $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                //order status if to be deleted
                var StatusList = new List<string> { "Open", "On Hold", "Rejected", "Expired" };

                var keys = new List<string> { salesOrder.OrderType, salesOrder.OrderNbr };

                var so = await client.GetByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(keys, select: "OrderType, OrderNbr, Status");
                if (so is null)
                {
                    var errorMessage = $@"Edit: Sales Order {so.OrderType} {so.OrderNbr} is not in a valid status for deletion. Status is {so.Status}.";
                    _logger.LogError(errorMessage);

                    return RedirectToPage("./Details");
                }
                else
                {
                    if (StatusList.Contains(so.Status))
                    {
                        infoMessage = $@"Edit: Sales Order {keys} is in a valid status for deletion. Status is {so.Status}.";
                        _logger.LogInformation(infoMessage);
                    }
                    else
                    {
                        var errorMessage = $@"Edit: Sales Order {keys} is not in a valid status for deletion. Status is {so.Status}.";
                        _logger.LogError(errorMessage);

                        return RedirectToPage("./Details");
                    }
                }

                var _so = new ConvertToSalesOrder(salesOrder);

                //Update the Sales Order using the updated record
                var result = client.Put<Acumatica.Default_24_200_001.Model.SalesOrder>(_so);
                if (result is not null)
                {
                    infoMessage = @$"Edit: Sales Order {_so.OrderNbr} edit status is {result}.";
                    _logger.LogInformation(infoMessage);
                }
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Edit: Failed to delete Sales Order exception {ex}.";
            _logger.LogError(errorMessage);

            return RedirectToPage("./Details");
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                infoMessage = $"Edit: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(infoMessage);
            }
            else
            {
                var errorMessage = $"Edit: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(errorMessage);
            }
        }

        SetParms();

        return RedirectToPage("./Details");
    }
    #endregion

    #region private methods
    private bool SalesOrder_AppExists(int id)
    {
        return (_context.SalesOrders?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private void GetParms()
    {
        Parms = JsonConvert.DeserializeObject<List<object>>((string)TempData["parms"]);
        if(Parms is null)
        {
            var errorMessage = $"Edit: No parameters exist. Please check your parameters!";
            _logger.LogError(errorMessage);
            throw new NullReferenceException(nameof(Parms));
        }

        FromDate = (DateTime)Parms[0];
        ToDate = (DateTime)Parms[1];
        NumRecords = Convert.ToInt32(Parms[2]);
        Selected_SalesOrder_Type = (string)Parms[3];

        var infoMessage = $"Edit: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(infoMessage);
    }

    private void SetParms()
    {
        Parms = [FromDate,
                           ToDate,
                           NumRecords,
                           Selected_SalesOrder_Type];
        if(Parms is null)
        {
            var errorMessage = $"Edit: No parameters exist. Please check your parameters!";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(Parms));
        }

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["EditFlag"] = true;
        TempData["DeleteFlag"] = false;

        var infoMessage = $"Edit: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}, EditFlag:true, DeleteFlag:false";
        _logger.LogInformation(infoMessage);
    }
    #endregion
}
#endregion