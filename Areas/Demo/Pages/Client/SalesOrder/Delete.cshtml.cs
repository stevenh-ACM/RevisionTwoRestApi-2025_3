#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Credential = RevisionTwoApp.RestApi.Models.Credential;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region DeleteModel
/// <summary>
/// Delete a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class DeleteModel(AppDbContext context, ILogger<DeleteModel> logger): PageModel
{
    #region ctor
    private readonly ILogger<DeleteModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly string _className = nameof(DeleteModel);
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

    /// <summary>
    /// Gets or sets the collection of sales orders associated with the application.
    /// </summary>
    public List<SalesOrder_App> SalesOrders { get; set; } = default!;

    /// <summary>
    /// Gets or sets the credentials used to authenticate with the site.
    /// </summary>
    public Site_Credential SiteCredential { get; set; }

    /// <summary>
    /// Gets or sets the credential information used for authentication.
    /// </summary>
    public Credential credential { get; set; }

    public DateTime FromDate { get; private set; }
    public DateTime ToDate { get; private set; }
    public int NumRecords { get; private set; }
    public string Selected_SalesOrder_Type { get; private set; }
    #endregion

    #region methods

    /// <summary>
    /// Handles the GET request to retrieve a SalesOrder by its ID for deletion.
    /// </summary>
    /// <param name="id">The ID of the SalesOrder to retrieve.</param>
    /// <returns>An IActionResult that renders the page if successful, or NotFound if the ID is invalid.</returns>
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            var errorMessage = $"{_className}: Id {id} or SalesOrder context {_context.ContextId} is null";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        // retrieve the sales order from the database
        salesOrder = await _context.SalesOrders.FirstOrDefaultAsync(m => m.Id == id);
        if (salesOrder is null)
        {
            var errorMessage = $"{_className}: SalesOrder with id {id} not found";
            _logger.LogError(errorMessage);

            return NotFound();
        }
        else
        {
            var infoMessage = $@"{_className}: SalesOrder found. {salesOrder}";
            _logger.LogInformation(infoMessage);
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
        SalesOrder = await _context.SalesOrders.FindAsync(id);
        if (SalesOrder is null)
        {
            var errorMessage = $"{_className}: No SalesOrder with ID {id} exists.";
            _logger.LogError(errorMessage);

            return NotFound();
        }

        // get current Acumatica ERP credentials to login
        SiteCredential = new(_context,_logger);

        credential = SiteCredential.GetSiteCredential().Result;
        if (credential is null)
        {
            var errorMessage = $"{_className}: No credentials found. Please create at least one credential.";
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
            var errorMessage = $"{_className}: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(client));
        }

        // attempt to delete the selected Sales Order from Acumatica ERP and ensure logout
        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName,credential.Password,"","","");
            if (client.RequestInterceptor is null)
            {
                var errorMessage = $"{_className}: Failure to create a context for client login: UserName of " +
                                         $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                //order status if to be deleted
                var StatusList = new List<string> { "Open","On Hold","Rejected","Expired" };

                var keys = new List<string> { SalesOrder.OrderType,SalesOrder.OrderNbr };

                var so = await client.GetByKeysAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(keys,select: "OrderType, OrderNbr, Status");
                if (so is null)
                {
                    var errorMessage = $@"{_className}: Sales Order {keys} not found in Acumatica ERP.";
                    _logger.LogError(errorMessage);

                    throw new NullReferenceException(nameof(so));
                }
                else
                {
                    if (StatusList.Contains(so.Status))
                    {
                        // chceck salseOrder status for deletion eligibility
                        var infoMessage = $"{_className}: Sales Order {keys} is in a valid status for deletion. Status is {so.Status}.";
                        _logger.LogInformation(infoMessage);
                    }
                    else
                    {
                        // if the status is not in the list, we cannot delete the Sales Order
                        var errorMessage = $"{_className}: Sales Order {so.OrderType} {so.OrderNbr} is not in a valid status for deletion. Status is {so.Status}.";
                        _logger.LogError(errorMessage);

                        //go back to calling page
                        return RedirectToPage("./Details");
                    }
                }

                //Delete the Sales Order using the keys
                var result = client.DeleteAsync<Acumatica.Default_24_200_001.Model.SalesOrder>(so);
                if (result is not null)
                {
                    var infoMessage = $"{_className}: Sales Order {so.OrderNbr} deletion status is {result}.";
                    _logger.LogInformation(infoMessage);
                }

                // remove salesOrder from SalesOrder_App cache
                _context.Remove(SalesOrder);
                _context.SaveChanges();

                var infoMessage2 = $"{_className}: SalesOrder deleted from local store. {SalesOrder}";
                _logger.LogInformation(infoMessage2);

                // set the DeleteFlag to true to indicate that the Sales Order has been deleted from local store and Acumatica ERP
                Globals.SetGlobalProperty("DeleteFlag", true, _logger);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}: Failed to delete Sales Order exception {ex}.");
            return RedirectToPage("./Details");
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                var infoMessage = $"{_className}: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(infoMessage);
            }
            else
            {
                var errorMessage = $"{_className}: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(errorMessage);
            }
        }
        //DeleteFlag = true;

        return RedirectToPage("./Details");
    }
    #endregion
}
#endregion

