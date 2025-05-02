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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Helper;
using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

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

    [BindProperty]
    public SalesOrder_App SalesOrder_App { get; set; } = default!; // SalesOrder_App is the model for the Sales Order Razor Model
    [BindProperty]
    public SalesOrder_App salesOrder { get; set; } = new();
    [BindProperty]
    public Combo_Boxes Combo_Boxes { get; set; } = new();
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
        int cnt = 0;
        // Get current SalesOrders from database
        List<SalesOrder_App> SalesOrders_App = await _context.SalesOrders.ToListAsync();
        if (SalesOrders_App is null)
        {
            _logger.LogError("Create-OnGetAsync: No Sales Orders exist. Please create at least one Sales Order!");
            return Page();
        }

        // Populate Selected_SalesOrder_Customers
        Selected_SalesOrder_Customers.Clear();
        foreach (var salesOrder in SalesOrders_App)
        {
            cnt++;
            if (!string.IsNullOrEmpty(salesOrder.CustomerID))
            {
                Selected_SalesOrder_Customers.Add(new SelectListItem
                {
                    Value = salesOrder.CustomerID,
                    Text  = salesOrder.CustomerName ?? salesOrder.CustomerID
                });
            }
        }
        salesOrder.Id = cnt;
        salesOrder.Date = Date.AddDays(-1);
        salesOrder.ShipmentDate = Date;

        return Page();
    }

    /// <summary>
    /// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAsync()
    {

        DateTimeValue defaultDate = DateTime.Parse("2010-01-01T00:00:01.000");

        // Get current SalesOrders from database
        List<SalesOrder_App> SalesOrders_App = await _context.SalesOrders.ToListAsync();

        _context.Attach(SalesOrder_App).State = EntityState.Modified;

        if (!ModelState.IsValid || _context.SalesOrders == null || SalesOrders_App is null)
        {
            _logger.LogError("Create-OnGetAsync: No Sales Orders exist. Please create at least one Sales Order!");
            return Page();
        }

        // get current Acumatcia ERP credentials to login
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
                var CustomerID = SalesOrder_App.CustomerID;

                _logger.LogInformation($"Create: Retrieve customer using Customer ID {CustomerID}.");

                var customer = client.GetList<Customer>(top: 1,filter: "Status eq 'Active'",select: "CustomerID").Single();

                var so = client.Put(new Acumatica.Default_24_200_001.Model.SalesOrder()
                {
                    CustomerID = customer.CustomerID,
                    Date = DateTime.Now.AddDays(-1),
                    Details = new List<SalesOrderDetail>()
                    {
                        new SalesOrderDetail()
                        {
                            InventoryID = InventoryID,
                            OrderQty = SalesOrder_App.OrderedQty,

                        }
                    }
                },expand: "Details");

                var shipment = client.Put(new Shipment()
                {
                    CustomerID = customer.CustomerID,
                    WarehouseID = so.Details!.Single().WarehouseID,
                    Details = new List<ShipmentDetail>()
                    {
                        new ShipmentDetail()
                        {
                            OrderNbr = so.OrderNbr,
                            OrderType = so.OrderType,
                            OrderLineNbr = so.Details!.First().LineNbr,
                        }
                    }
                });
                
                var baAccount = client.GetByKeys<BusinessAccount>([CustomerID]);
                if(baAccount is null)
                {
                    var Message = $"Details: Failure to determine Business Account using {client.ToString} and CustomerID {CustomerID}";
                    _logger.LogError(Message);
                    throw new NullReferenceException(nameof(baAccount));
                }

                var _so = new ConvertToSO(so,baAccount,shipment.ShipmentDate); // Create App Sales Order from Acumatica Sales Order

                await _context.SalesOrders.AddAsync(_so);
                await _context.SaveChangesAsync();  

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