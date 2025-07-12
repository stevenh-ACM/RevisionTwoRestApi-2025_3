#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region CreateModel
/// <summary>
/// Represents the page model for creating new sales orders in the application.
/// </summary>
/// <remarks>This class provides properties and methods to manage the creation of sales orders, including handling
/// user input, interacting with the database, and integrating with external APIs. It supports both GET and POST
/// operations for initializing and submitting sales order data.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class CreateModel(AppDbContext context, ILogger<CreateModel> logger) : PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModel"/> class.
    /// </summary>
    private readonly ILogger<CreateModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    #endregion

    #region properties
    /// <summary>
    /// Represents the credentials required to access the site.
    /// </summary>
    public Site_Credential SiteCredential { get; set; }
    /// <summary>
    /// Gets the list of sales order types available for selection.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Types { get; } = new Combo_Boxes().ComboBox_SalesOrder_Types;
    /// <summary>
    /// Gets a list of selectable sales order statuses for use in UI components.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Statuses { get; } = new Combo_Boxes().ComboBox_SalesOrder_Statuses;
    /// <summary>
    /// Gets a list of customers available for selection in a sales order.
    /// </summary>
    public List<SelectListItem> Selected_SalesOrder_Customers { get; } = [];
    /// <summary>
    /// Gets or sets the collection of sales orders associated with the application.
    /// </summary>
    public List<SalesOrder_App> SalesOrders { get; set; } = default!;

    /// <summary>
    /// Gets or sets the sales order associated with the current operation.
    /// </summary>
    [BindProperty]
    public SalesOrder_App salesOrder { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="Combo_Boxes"/> instance used to manage the state and data of combo box controls.
    /// </summary>
    [BindProperty]
    public Combo_Boxes Combo_Boxes { get; set; } = new();

    /// <summary>
    /// Gets or sets the date value associated with the current operation.
    /// </summary>
    [BindProperty]
    public DateTime Date { get; set; } = DateTime.Now;
    /// <summary>
    /// Gets or sets the unique identifier for the inventory item.
    /// </summary>
    [BindProperty]
    public string InventoryID { get; set; } = "AACOMPUT01";
    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    [BindProperty]
    public DateTime lastModified { get; set; } = DateTime.Now;

    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests for the page and initializes data required for rendering.
    /// </summary>
    /// <remarks>This method retrieves the current list of sales orders from the database and populates the 
    /// <see cref="Selected_SalesOrder_Customers"/> collection with customer information for use in the UI. If no sales
    /// orders exist, an error is logged and the page is returned without additional data.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Typically, this will return the page 
    /// to the caller.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        // Get current SalesOrders from local store
        var customers = await _context.Customers.ToListAsync();
        if (customers is null)
        {
            _logger.LogError("Create: No customers exist.");
            throw new NullReferenceException(nameof(customers));
        }

        // Populate Selected_SalesOrder_Customers
        Selected_SalesOrder_Customers.Clear();
        foreach (var customer in customers)
        {
            if (!string.IsNullOrEmpty(customer.CustomerID))
            {
                Selected_SalesOrder_Customers.Add(new SelectListItem
                {
                    Value = customer.CustomerID,
                    Text  = customer.CustomerName
                });
            }
        }

        salesOrder.Date = Date.AddDays(-1);
        salesOrder.ShipmentDate = Date;
        salesOrder.CurrencyID = "USD";
        salesOrder.LastModified = DateTime.Now;

        return Page();
    }

    /// <summary>
    /// Handles the HTTP POST request to create a new sales order.
    /// </summary>
    /// <remarks>This method performs several operations, including validating the model state, retrieving sales
    /// orders from the database,  adjusting sales order values, and interacting with an external API to create and post the
    /// sales order.  If the operation is successful, the new sales order is added to the database and the user is
    /// redirected to the index page. If any errors occur during the process, the method logs the error and returns the
    /// current page.</remarks>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.  Returns a <see
    /// cref="RedirectToPageResult"/> if the sales order is successfully created and posted;  otherwise, returns a <see
    /// cref="PageResult"/> indicating an error or invalid state.</returns>
    /// <exception cref="NullReferenceException">Thrown if required objects, such as the API client or business account, are null during the operation.</exception>
    public async Task<IActionResult> OnPostAsync()
    {        
        _context.Attach(salesOrder).State = EntityState.Added;
        
        if (!ModelState.IsValid)
        {
            var Message = $"Create: No Sales Orders exist. Please create at least one Sales Order!";
            _logger.LogError(Message);
            return Page();
        }

        // Find the CustomerID by CustomerName from the Customers table
        Customer_App customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerID == salesOrder.CustomerName);
        if (customer == null)
        {
            var message = $@"Create: No customer found with name {salesOrder.CustomerName}.";
            _logger.LogError(message);
            ModelState.AddModelError(string.Empty,message);
            return Page();
        }

        salesOrder.CustomerID = customer.CustomerID;
        salesOrder.CustomerName = customer.CustomerName;
      
        // get current Acumatica ERP credentials to login
        Site_Credential SiteCredential = new(_context,_logger);

        Credential credential = SiteCredential.GetSiteCredential().Result;

        if(credential == null)
        {
            var Message = "Create: No credentials found. Please create at least one credential.";
            _logger.LogError(Message);
            return Page();
        }

        var client = new ApiClient(credential.SiteUrl,
            requestInterceptor: RequestLogger.LogRequest,
            responseInterceptor: RequestLogger.LogResponse,
            ignoreSslErrors: true // this is here to allow testing with self-signed certificates
            );

        if(client.RequestInterceptor is null)
        {
            var Message = $@"Create: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(client));
        }

        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName,credential.Password,"","","");
            if(client.RequestInterceptor is null)
            {
                var Message = $@"Create: Failure to create a context for client login: UserName of " +
                                    $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(Message);
                throw new NullReferenceException(nameof(client));
            }
            else
            {
                var so = client.Put(new Acumatica.Default_24_200_001.Model.SalesOrder()
                {
                    CustomerID = salesOrder.CustomerID,
                    Date = salesOrder.Date,
                    Details =
                    [
                        new SalesOrderDetail()
                        {
                            InventoryID = InventoryID,
                            OrderQty = salesOrder.OrderedQty,

                        }
                    ]
                },expand: "Details");

                var Message = $"Create: New Sales Order added to {credential.SiteUrl}. The Order placed is {so}."; 
                _logger.LogInformation(Message);

               var shipment = new Shipment { 
                    ShipmentDate = salesOrder.ShipmentDate
               }; 

                Message = $"Create: New Shipment is added to {credential.SiteUrl}. The Shipment Created is {shipment}.";
                _logger.LogInformation(Message);

                var baAccount = client.GetByKeys<BusinessAccount>([ salesOrder.CustomerID ]);
                if (baAccount is null)
                {
                    Message = $"Create: Failure to determine Business Account using {client.ToString} and CustomerID {salesOrder.CustomerID}";
                    _logger.LogError(Message);
                    throw new NullReferenceException(nameof(baAccount));
                }

                Message = $"Create: Convert Sales Order {so} + BAccountID {baAccount.BusinessAccountID} + Shipment Date {shipment.ShipmentDate} to SalesOrder_App.";
                _logger.LogInformation(Message);

                var _so = new ConvertToSO(so, baAccount, shipment.ShipmentDate); // Create App Sales Order from Acumatica Sales Order

                Message= $@"Create: Convert Sales Order to SalesOrder_App {_so}.";
                _logger.LogInformation(Message);

                // adjusted salesOrder added to the SalesOrder_App cache
                var SalesOrders = await _context.SalesOrders.ToListAsync();
                await _context.SalesOrders.AddAsync(_so);
                await _context.SaveChangesAsync();

                Message = $"Create: Sales Order Created and posted and added to SalesOrder_App cache.";
                _logger.LogInformation(Message);

                return RedirectToPage("./Index");
            }
        }
        catch(Exception ex)
        {
            var Message = $"Create: Error creating Sales Order: {ex.Message}";
            _logger.LogError(ex,Message);
            return Page();
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                var Message = $"Create: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(message: Message);
            }
            else
            {
                var Message = $"Create: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(Message);
            }
        }
    }
    #endregion
}
#endregion  