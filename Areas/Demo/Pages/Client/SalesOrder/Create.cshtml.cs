#nullable disable

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;

using Humanizer.DateTimeHumanizeStrategy;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Helper;
using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

/// <summary>
/// Create a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class CreateModel(AppDbContext context, ILogger<CreateModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<CreateModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region properties

    public Site_Credential SiteCredential { get; set; }
    public List<SelectListItem> Selected_SalesOrder_Types { get; } = new Combo_Boxes().ComboBox_SalesOrder_Types;
    public List<SelectListItem> Selected_SalesOrder_Statuses { get; } = new Combo_Boxes().ComboBox_SalesOrder_Statuses;
    public List<SelectListItem> Selected_SalesOrder_Customers { get; } = [];
    public List<SalesOrder_App> SalesOrders { get; set; } = default!;

    [BindProperty]
    public SalesOrder_App salesOrder { get; set; } = new();
    [BindProperty]
    public Combo_Boxes Combo_Boxes { get; set; } = new();

    // default values for new Sales Order 
    [BindProperty]
    public DateTime Date { get; set; } = DateTime.Now;
    [BindProperty]
    public string InventoryID { get; set; } = "AACOMPUT01";
    [BindProperty]
    public DateTime lastModified { get; set; } = DateTime.Now;

    #endregion

    #region methods

    public async Task<IActionResult> OnGetAsync()
    {
        // Get current SalesOrders from cache 
        SalesOrders = await _context.SalesOrders.ToListAsync();
        if (SalesOrders is null)
        {
            _logger.LogError("Create-OnGetAsync: No Sales Orders exist. Please create at least one Sales Order!");
            return Page();
        }

        // Populate Selected_SalesOrder_Customers
        Selected_SalesOrder_Customers.Clear();
        foreach (var salesOrder in SalesOrders)
        {
            if (!string.IsNullOrEmpty(salesOrder.CustomerID))
            {
                Selected_SalesOrder_Customers.Add(new SelectListItem
                {
                    Value = salesOrder.CustomerID,
                    Text  = salesOrder.CustomerName
                });
            }
        }

        salesOrder.Date = Date.AddDays(-1);
        salesOrder.ShipmentDate = Date;

        return Page();
    }

    /// <summary>
    /// To protect from over posting attacks, see https://aka.ms/RazorPagesCRUD
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAsync()
    {
        // Get current SalesOrders from database
        SalesOrders = await _context.SalesOrders.ToListAsync();
 
        if (!ModelState.IsValid || _context.SalesOrders == null || SalesOrders == null)
        {
            _logger.LogError("Create-OnGetAsync: No Sales Orders exist. Please create at least one Sales Order!");
            return Page();
        }

        _context.Attach(salesOrder).State = EntityState.Added;
        
        // adjust values not captured on Screen or are not sensible for a create screen
        //salesOrder.CustomerID = salesOrder.CustomerName;
        salesOrder.CustomerName = SalesOrders.Find(x => x.CustomerID == salesOrder.CustomerID).CustomerName;
        salesOrder.CurrencyID = SalesOrders.Find(x => x.CustomerID == salesOrder.CustomerID).CurrencyID;
        salesOrder.LastModified = DateTime.Now;

        // add adjusted salesOrder or the SalesOrder_App cache
        SalesOrders.Add(salesOrder);
        
        // get current Acumatica ERP credentials to login
        Site_Credential SiteCredential = new(_context,_logger);

        Credential credential = SiteCredential.GetSiteCredential().Result;

        if(credential == null)
        {
            _logger.LogError("Create-OnPostAsync: No credentials found. Please create at least one credential.");
            return Page();
        }

        var client = new ApiClient(credential.SiteUrl,
                                   requestInterceptor: RequestLogger.LogRequest,
                                   responseInterceptor: RequestLogger.LogResponse,
                                   ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                   );

        if(client.RequestInterceptor is null)
        {
            var Message = $"Create: Failure to create a RestAPI client to Site {credential.SiteUrl} ";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(client));
        }

        try
        {
            //RestClient Log In (on) using Credentials retrieved
            //authApi.LogIn( Credentials[id].UserName, Credentials[id].Password, "", "", "" );
            client.Login(credential.UserName,credential.Password,"","","");
            if(client.RequestInterceptor is null)
            {
                var Message = $"Create: Failure to create a context for client login: UserName of " +
                              $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(Message);
                throw new NullReferenceException(nameof(client));
            }
            else
            {
                var CustomerID = salesOrder.CustomerID;
                if (string.IsNullOrEmpty(CustomerID))
                {
                    _logger.LogError("Create: CustomerID is null or empty. Cannot proceed with Sales Order creation.");
                    return Page();
                }
                else 
                {
                    _logger.LogInformation($"Create: Retrieve customer using Customer ID {CustomerID}.");

                }

                //var customer = client.GetList<Customer>(top: 1,filter: "Status eq 'Active'",select: "CustomerID").Single();
                var customer = client.GetByKeys<Customer>([CustomerID]);
                if (customer is null)
                {
                    var Message = $"Create: Failure to determine Customer using {client.ToString()} and CustomerID {CustomerID}";
                    _logger.LogError(Message);
                    throw new NullReferenceException(nameof(customer));
                }
                else 
                {
                    _logger.LogInformation($"Create: Retrieved Customer details {customer}.");
                }

                _logger.LogInformation($"Create: Put Customer details");

                var so = client.Put(new Acumatica.Default_24_200_001.Model.SalesOrder()
                {
                    CustomerID = customer.CustomerID,
                    Date = DateTime.Now.AddDays(-1),
                    Details =
                    [
                        new SalesOrderDetail()
                        {
                            InventoryID = InventoryID,
                            OrderQty = salesOrder.OrderedQty,

                        }
                    ]
                },expand: "Details");

                _logger.LogInformation($"Create: New Sales Order added to {credential.SiteUrl}. The Order placed is {so}.");

                //var shipment = client.Put(new Shipment()
                //{
                //    CustomerID = customer.CustomerID,
                //    WarehouseID = so.Details!.Single().WarehouseID,
                //    Details =
                //    [
                //        new ShipmentDetail()
                //        {
                //            OrderNbr = so.OrderNbr,
                //            OrderType = so.OrderType,
                //            OrderLineNbr = so.Details!.First().LineNbr,
                //        }
                //    ]
                //});

               var shipment = new Shipment { 
                    ShipmentDate = DateTime.Now.AddDays(-1)
                }; 

                _logger.LogInformation($"Create: New Shipment is added to {credential.SiteUrl}. The Shipment Created is {shipment}.");

                var baAccount = client.GetByKeys<BusinessAccount>([CustomerID]);
                if (baAccount is null)
                {
                    var Message = $"Details: Failure to determine Business Account using {client.ToString} and CustomerID {CustomerID}";
                    _logger.LogError(Message);
                    throw new NullReferenceException(nameof(baAccount));
                }

                _logger.LogInformation($"Create: Convert Sales Order {so} + BAccountID {baAccount.BusinessAccountID} + Shipment Date {shipment.ShipmentDate} to SalesOrder_App.");

                var _so = new ConvertToSO(so, baAccount, shipment.ShipmentDate); // Create App Sales Order from Acumatica Sales Order

                _logger.LogInformation($"Create: Newly created SalesOrder_App record to be added {_so}.");

                //Update app cache with newly created record
                await _context.SalesOrders.AddAsync(_so);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Create: Sales Order Created and posted and added to SalesOrder_App cache.");

                return RedirectToPage("./Index");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"Create-OnPostAsync: Error creating Sales Order.");
            return Page();
        }
    }
    #endregion
}