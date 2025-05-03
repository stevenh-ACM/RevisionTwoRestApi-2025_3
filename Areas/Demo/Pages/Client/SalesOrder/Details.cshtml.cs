#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

/// <summary>
/// Details a new Sales Order
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class DetailsModel(AppDbContext context,ILogger<DetailsModel> logger) : PageModel
{
    #region ctor

    private readonly ILogger<DetailsModel> _logger = logger;
    private readonly AppDbContext _context = context;

    #endregion

    #region properties
    /// <summary>
    /// Credentials list of creds in db
    /// </summary>
    public List<Acumatica.Default_24_200_001.Model.SalesOrder> salesOrders { get; set; } // salesOrder ERP data set

    public List<SalesOrderShipment> salesOrderShipment { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of sales orders retrieved from the database or API.
    /// </summary>
    public List<SalesOrder_App> SalesOrders_App { get; set; } = []; // SalesOrders data set

    /// <summary>
    /// Gets or sets the parameters used for filtering or other operations.
    /// </summary>
    public List<object> Parms { get; set; } = [new()];

    /// <summary>
    /// Gets or sets the site credentials object used to hold credentials for Acumatica ERP connection.
    /// </summary>
    public Site_Credential SiteCredential { get; set; } // site object to hold credentials for Acumatica ERP connection

    /// <summary>
    /// Business Account ID 
    /// </summary>
    public BusinessAccount BAccount { get; set; }

    /// <summary>
    /// Variable
    /// </summary>
    public int Id { get; set; } // parameter to pass to Edit or Delete

    /// <summary>
    /// Indicates whether the page is simply being refreshed.
    /// </summary>
    public Boolean RefreshFlag { get; set; } = false; // is the page simply being refreshed

    /// <summary>
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering sales orders.
    /// </summary>
    public DateTime ToDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the number of records to retrieve.
    /// </summary>
    public int NumRecords { get; set; }

    /// <summary>
    /// Gets or sets the selected sales order type.
    /// </summary>
    public string Selected_SalesOrder_Type { get; set; }

    /// <summary>
    /// List of sales order shipments
    /// </summary>
    public List<SalesOrderShipment> SalesOrderShipment { get; set; } = new List<SalesOrderShipment>();

    /// <summary>
    /// List of sales order shipments
    /// </summary>
    public List<SalesOrderShipment> SalesOrderShipments { get; set; } = new List<SalesOrderShipment>();

    #endregion

    #region methods
    /// <summary>
    /// Retrieve list of the most recent 10 Orders, type SO
    /// </summary>
    /// 
    public async Task<IActionResult> OnGetAsync()
    {
        GetParms();

        // main redirect check
        if((bool)TempData["EditFlag"]) //|| (bool)TempData["DeleteFlag"])
        {
            // refresh cache with any changes made in Edit or Delete
           SalesOrders_App = await _context.SalesOrders.ToListAsync();
           RefreshFlag = true;

            if(SalesOrders_App.Count == 0)
            {
                var Message = $"Details: No SalesOrders found in the cache!";
                _logger.LogError(Message);

                return RedirectToPage("/Index");
            }
        }
        else
        {
            // clear out dB from previous retrievals
            _context.SalesOrders.RemoveRange();
            _context.SaveChanges();
        }
       
        if(!RefreshFlag) // not a refresh then retrieve records
        {
            Site_Credential SiteCredential = new(_context, _logger);

            Credential credential = SiteCredential.GetSiteCredential().Result;

            // httpClient RestClient created with baseURL assigned
            var client = new ApiClient(credential.SiteUrl,
                                               requestInterceptor: RequestLogger.LogRequest,
                                               responseInterceptor: RequestLogger.LogResponse,
                                               ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                               );

            if(client is null)
            {
                var Message = $"Details: Failure to create an client context. SiteURL is {credential.SiteUrl}";
                _logger.LogError(message: Message);
                throw new NullReferenceException(nameof(client));
            }
            try
            {
                //RestClient Log In (on) using Credentials retrieved
                //authApi.LogIn( Credentials[id].UserName, Credentials[id].Password, "", "", "" );
                client.Login(credential.UserName,credential.Password,"","","");

                if(client.RequestInterceptor is null)
                {
                    var Message2 = $"Details: Failure to create an configuration context. client login has UserName of " +
                                $"{credential.UserName} and Password of {credential.Password}";
                    _logger.LogError(Message2);
                    throw new NullReferenceException(nameof(client));
                }
                else
                {
                    Console.WriteLine("Reading Accounts...");
                    var Message3 = "Details: Reading Accounts for parameters chosen";
                    _logger.LogInformation(Message3);
                }

                //Rest parameters for API methods
                var fromDateTimeOffset = FromDate.ToString("s");
                var toDateTimeOffset = ToDate.ToString("s");
                string filter = @$"Date gt datetimeoffset'{fromDateTimeOffset}' and Date le datetimeoffset'{toDateTimeOffset}' and OrderType eq '{Selected_SalesOrder_Type}'";
                string select = "OrderType,OrderNbr,Status,Date,Shipments/ShipmentDate,CustomerID,OrderedQty,OrderTotal,CurrencyID, LastModified";
                string expand = "Shipments";
                string custom = null;
                int? skip = null;
                int? top = NumRecords;

                //Invoke GetList (of SalesOrders per parameters) on SalesOrder Api
                var salesOrders = client.GetList<Acumatica.Default_24_200_001.Model.SalesOrder>(null,select,filter,expand,custom,skip,top);

                DateTimeValue defaultDate = DateTime.Parse("2010-01-01T00:00:01.000");

                for(int idx = 0;idx < (salesOrders.Count);idx++)
                {
                    if(salesOrders[idx].Shipments.Count == 0)
                    {
                        salesOrderShipment.Add(new SalesOrderShipment());
                        salesOrders[idx].Shipments.AddRange(salesOrderShipment);
                        salesOrders[idx].Shipments[0].ShipmentDate = defaultDate;
                    }

                    string[] customerID = [salesOrders[idx].CustomerID];
                    var baAccount = client.GetByKeys<BusinessAccount>(customerID);
                    if(baAccount is null)
                    {
                        var Message2 = $"Details: Failure to create an salesOrderApi using {client.ToString}";
                        _logger.LogError(Message2);
                        throw new NullReferenceException(nameof(baAccount));
                    }

                    //Create an data model using field values from Api calls
                    var _so = new ConvertToSO(salesOrders[idx],baAccount,salesOrders[idx].Shipments[0].ShipmentDate);

                    //Add data model record list
                    await _context.SalesOrders.AddAsync(_so);
                   SalesOrders_App.Add(_so);
                }

                var Message = $"Details:SalesOrders has {SalesOrders_App.Count} records";
                _logger.LogInformation(Message);

                //Write list of records accumulated to dB
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                var Message = $"Details: Exception caught {e.Message}";
                _logger.LogError(Message);
            }
            finally
            {
                //we use logout in finally block because we need to always log out, even if the request failed for some reason
                if(client.TryLogout())
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
        }
        // set Model to current result setSalesOrders for display
        if(!ModelState.IsValid)
        {
             var Message = $"Details: No SalesOrders found in the cache!";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(SalesOrders_App));
        }

        SetParms();

        return Page();
    }

    /// <summary>
    /// Handles the POST request for the Details page.
    /// Retrieves sales orders filtered by the specified date range and updates the model.
    /// </summary>
    /// <returns>An IActionResult representing the result of the POST operation.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var Message = $"Details: From Date is {FromDate}: To Date is {ToDate}";
        _logger.LogInformation(Message);

        SalesOrders_App = await _context.SalesOrders.Where(x => x.Date >= FromDate && x.Date <= ToDate).ToListAsync();

        SetParms();

        return Page();
    }
    #endregion

    #region parameters

    private void GetParms()
    {
        Parms = JsonConvert.DeserializeObject<List<object>>((string)TempData["parms"]);
        if(Parms is null)
        {
            var Message4 = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(Message4);
            throw new NullReferenceException(nameof(Parms));
        }

        FromDate = (DateTime)Parms[0];
        ToDate = (DateTime)Parms[1];
        NumRecords = Convert.ToInt32(Parms[2]);
        Selected_SalesOrder_Type = (string)Parms[3];

        var Message = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(Message);

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
            var Message5 = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(Message5);
            throw new NullReferenceException(nameof(Parms));
        }

        RefreshFlag = false;

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["EditFlag"] = false;
        TempData["DeleteFlag"] = false;

        var Message = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(Message);

        return;
    }
    #endregion
}