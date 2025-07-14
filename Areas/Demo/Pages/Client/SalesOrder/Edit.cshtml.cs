#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML

using Microsoft.AspNetCore.Mvc.Rendering;

using Credential = RevisionTwoApp.RestApi.Models.Credential;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region EditModel
/// <summary>
/// Represents the model for editing sales orders in the application.
/// </summary>
/// <remarks>The <see cref="EditModel"/> class provides functionality for handling HTTP GET and POST requests 
/// related to editing sales orders. It includes properties for binding sales order data, managing  selectable options,
/// and tracking parameters for filtering and processing sales orders.  This model is designed to interact with the
/// application's database context and logging system.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class EditModel(AppDbContext context, ILogger<EditModel> logger) : PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="EditModel"/> class.
    /// </summary>
    private readonly ILogger<EditModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly string _className = nameof(EditModel);
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the sales order application model used for binding and processing sales order data.
    /// </summary>
    [BindProperty]
    public SalesOrder_App SalesOrder { get; set; }

    Site_Credential SiteCredential { get; set; }

    Credential credential { get; set; }

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
        if (id is null)
        {
            var errorMessage = $"{_className}: Id {id}";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        //Retrieve the SalesOrder from the local store using id
        SalesOrder = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);
        if (SalesOrder is null)
        {
            var errorMessage = $"{_className}: Edit of id {id} failed";
            _logger.LogError(errorMessage);

            return NotFound();
        }
        else
        {
            SalesOrder.LastModified = DateTime.Now;
            SalesOrder.ShipmentDate = DateTime.Now.AddDays(1);
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
            var Errors = ModelState.Values.SelectMany(v => v.Errors);

            var errorMessage = $@"{_className}: Model State is inValid for Id {id} ModelState Values are:{Errors}";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        var infoMessage = $@"{_className}: Edited SalesOrder is {SalesOrder}";
        _logger.LogInformation(infoMessage);

        // get current Acumatica ERP credentials to login
        SiteCredential = new(_context, _logger);

        credential = SiteCredential.GetSiteCredential().Result;
        if (credential is null)
        {
            var errorMessage = $@"{_className}: No credentials found. Please create at least one credential.";
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
            var errorMessage = $@"{_className}: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
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
                var errorMessage = $@"{_className}: Failure to create a context for client login: UserName of " +
                                         $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                //order status if to be deleted
                var StatusList = new List<string> { "Open", "On Hold", "Rejected", "Expired" };

                var keys = new List<string> { SalesOrder.OrderType, SalesOrder.OrderNbr };

                var so = await client.GetByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(keys, select: "OrderType, OrderNbr, Status");
                if (so is null)
                {
                    var errorMessage = $@"{_className}: Sales Order with keys {keys} doesn't exist.";
                    _logger.LogError(errorMessage);

                    return RedirectToPage("./Details");
                }
                else
                {
                    if (StatusList.Contains(so.Status))
                    {
                        infoMessage = $"{_className}: Sales Order {keys} is in a valid status for editing. Status is {so.Status}.";
                        _logger.LogInformation(infoMessage);
                    }
                    else
                    {
                        var errorMessage = $@"{_className}: Sales Order {keys} is not in a valid status for editing. Status is {so.Status}.";
                        _logger.LogError(errorMessage);

                        return RedirectToPage("./Details");
                    }
                }

                var _so = new ConvertToSalesOrder(SalesOrder);

                //Update the Sales Order using the updated record
                var result = client.Put<Acumatica.Default_24_200_001.Model.SalesOrder>((_so));
                if (result is not null)
                {
                    infoMessage = $"{_className}: Sales Order {_so.OrderNbr} edit status is {result}.";
                    _logger.LogInformation(infoMessage);
                }

                // Update the SalesOrder property
                _context.Attach(SalesOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync(); //Save Modified Record to local store

                // sales order is edited, set the EditFlag to true
                Globals.SetGlobalProperty("EditFlag", true, _logger);

                return RedirectToPage("./Details");
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"{_className}: Failed to delete Sales Order exception {ex}.";
            _logger.LogError(errorMessage);

            return RedirectToPage("./Details");
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                infoMessage = $"{_className}: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(infoMessage);
            }
            else
            {
                var errorMessage = $"{_className}: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(errorMessage);
            }
        }
    }
    #endregion
}
#endregion