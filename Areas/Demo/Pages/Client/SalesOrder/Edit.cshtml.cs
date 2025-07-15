#nullable disable

//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
//#pragma warning disable CS1572 // XML comment has badly formed XML

using Microsoft.AspNetCore.Mvc.Rendering;
using Credential = RevisionTwoApp.RestApi.Models.Credential;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region EditModel
/// <summary>
/// Represents the model for editing sales orders in the application.
/// </summary>
/// <remarks>The <see cref="EditModel"/> class provides properties and methods for handling sales order data,
/// including retrieving, updating, and validating sales orders. It is designed to work with ASP.NET Core Razor Pages
/// and integrates with the application's database context and logging system.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
[BindProperties]
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
    public SalesOrder_App SalesOrder { get; set; }

    /// <summary>
    /// Gets or sets the Credentials used to authenticate with the site.
    /// </summary>
    Site_Credential SiteCredential { get; set; }

    /// <summary>
    /// Gets or sets the Credential used for authentication or authorization.
    /// </summary>
    Credential Credential { get; set; }

    /// <summary>
    /// Gets or sets the starting date for the specified time range.
    /// </summary>
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time value representing the target date.
    /// </summary>
    public DateTime ToDate { get; set; }

    /// <summary>
    /// Gets or sets the number of records currently stored or processed.
    /// </summary>
    public int NumRecords { get; set; }
    /// <summary>
    /// Gets or sets the type of the selected sales order.
    /// </summary>
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

        // get current Acumatica ERP Credentials to login
        SiteCredential = new(_context, _logger);

        Credential = SiteCredential.GetSiteCredential().Result;
        if (Credential is null)
        {
            var errorMessage = $@"{_className}: No Credentials found. Please create at least one Credential.";
            _logger.LogError(errorMessage);

            return RedirectToPage("Demo/Credentials");
        }

        var client = new ApiClient(
                                    Credential.SiteUrl,
                                            requestInterceptor: RequestLogger.LogRequest,
                                            responseInterceptor: RequestLogger.LogResponse,
                                            ignoreSslErrors: true); // this is here to allow testing with self-signed certificates
        if (client.RequestInterceptor is null)
        {
            var errorMessage = $@"{_className}: Failure to create a RestAPI client to Site {Credential.SiteUrl} ";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(client));
        }

        // attempt to delete the selected Sales Order from Acumatica ERP and ensure logout
        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(Credential.UserName, Credential.Password, "", "", "");
            if (client.RequestInterceptor is null)
            {
                var errorMessage = $@"{_className}: Failure to create a context for client login: UserName of " +
                                         $"{Credential.UserName} and Password of {Credential.Password}";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                //order status if to be deleted
                var StatusList = new List<string> { "Open", "On Hold", "Rejected", "Expired" };

                var keys = new List<string> { SalesOrder.OrderType, SalesOrder.OrderNbr };

                var so = await client.GetByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(keys, null, "OrderType, OrderNbr, Status");
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

                //Acumatica.Default_24_200_001.Model.SalesOrder _so = new ConvertToSalesOrder(SalesOrder);

                //Update the Sales Order using the updated record
                var result = await client.PutAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(new Acumatica.Default_24_200_001.Model.SalesOrder
                {
                    OrderNbr = so.OrderNbr,
                    OrderType = so.OrderType,
                    Status = SalesOrder.Status
                });
                //    Details = new List<SalesOrderDetail>()
                //    {
                //        new SalesOrderDetail()
                //        {
                //            InventoryID = (string)Globals.GetGlobalProperty("InventoryID"),
                //            ShipOn = so.ShipOn
                //        }
                //    }
                //}, expand: "Details");

                if (result is not null)
                {
                    infoMessage = $"{_className}: Sales Order {SalesOrder.OrderNbr} edit status is {result}.";
                    _logger.LogInformation(infoMessage);
                }

                // Update the SalesOrder property
                _context.Attach(SalesOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync(); //Save Modified Record to local store

                // sales order is edited, set the EditFlag to true
                Globals.SetGlobalProperty("EditFlag", true, _logger);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"{_className}: Failed to Edit Sales Order exception {ex}.";
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

        return RedirectToPage("./Details");
    }
    #endregion
}
#endregion