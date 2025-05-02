#nullable disable

using System.ComponentModel.DataAnnotations;

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

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
    public List<Credential> Creds { get; set; } = [];  //URL information to ERP site
    public List<Acumatica.Default_24_200_001.Model.SalesOrder> salesOrders { get; set; } // salesOrder ERP data set
    public List<SalesOrderShipment> salesOrderShipment { get; set; } = [];
    public List<SalesOrder_App> salesOrder_app { get; set; } = []; // salesOrder_app data set

    public List<object> Parms { get; set; } = [new()];
    public string Message { get; set; }

    public BusinessAccount BAccount { get; set; }

    // Model parameters from Index or Filter to retrieve ERP records or filter already retrieved records
    [BindProperty]
    public List<SalesOrder_App> salesOrders_App { get; set; } = [new()]; //salesOrder_App context
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime ToDate { get; set; } = DateTime.Now;
    [BindProperty]
    public int NumRecords { get; set; }
    [BindProperty]
    public string Selected_SalesOrder_Type { get; set; }

    public int id { get; set; } // parameter to pass to Edit or Delete
    public Boolean refreshFlag { get; set; } = false; // is the page simply being refreshed


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
            salesOrder_app = await _context.SalesOrders.ToListAsync();
            refreshFlag = true;

            if(salesOrder_app.Count == 0)
            {
                Message = $"Details: No SalesOrders found in the cache!";
                _logger.LogError(Message);

                return RedirectToPage("Pages/Home/Index");
            }
        }
        else
        {
            // clear out dB from previous retrievals
            _context.SalesOrders.RemoveRange();
            _context.SaveChanges();

            //Retrieve Credentials from dB for ERP retrieval
            Creds = await _context.Credentials.ToListAsync();

            if(Creds is null)
            {
                Message = "Details: No Credentials exist. Please create at least one Credential!";
                _logger.LogError(Message);

                return RedirectToPage("Pages/Home/Index");
            }
            else
            {
                //Get the selected creds to access Acumatica ERP with
                int id = new AuthId().getAuthId(Creds) - 1;
                if(id is < 0)
                {
                    Message = "Details: No Credentials exist.Please create at least one Credential!";
                    _logger.LogError(Message);

                    return RedirectToPage("Pages/Credentials/Index");
                }
                Message = $"Details: Credential secured. SiteURL is {Creds[id].SiteUrl} and id is {id}";
                _logger.LogInformation(Message);
            }
        }

        if(!refreshFlag) // not a refresh then retrieve records
        {
            //httpClient RestClient created with baseURL assigned
            var client = new ApiClient(Creds[id].SiteUrl,
                                               requestInterceptor: RequestLogger.LogRequest,
                                               responseInterceptor: RequestLogger.LogResponse,
                                               ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                               );

            if(client is null)
            {
                Message = $"Details: Failure to create an client context. SiteURL is {Creds[id].SiteUrl} and id is {id}";
                _logger.LogError(message: Message);
                throw new NullReferenceException(nameof(client));
            }

            try
            {
                //RestClient Log In (on) using Credentials retrieved
                //authApi.LogIn( Credentials[id].UserName, Credentials[id].Password, "", "", "" );
                client.Login(Creds[id].UserName,Creds[id].Password,"","","");

                if(client.RequestInterceptor is null)
                {
                    Message = $"Details: Failure to create an configuration context. client login has UserName of " +
                              $"{Creds[id].UserName} and Password of {Creds[id].Password}";
                    _logger.LogError(Message);
                    throw new NullReferenceException(nameof(client));
                }
                else
                {
                    Console.WriteLine("Reading Accounts...");
                    Message = "Details: Reading Accounts for parameters chosen";
                    _logger.LogInformation(Message);
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
                        Message = $"Details: Failure to create an salesOrderApi using {client.ToString}";
                        _logger.LogError(Message);
                        throw new NullReferenceException(nameof(baAccount));
                    }

                    //Create an data model using field values from Api calls
                    var _so = new ConvertToSO(salesOrders[idx],baAccount,salesOrders[idx].Shipments[0].ShipmentDate);

                    //Add data model record list
                    _context.SalesOrders.Add(_so);
                    salesOrder_app.Add(_so);
                }

                Message = $"Details: salesOrder_app has {salesOrder_app.Count} records";
                _logger.LogInformation(Message);

                //Write list of records accumulated to dB
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Message = $"Details: Exception caught {e.Message}";
                _logger.LogError(Message);
            }
            finally
            {
                //we use logout in finally block because we need to always log out, even if the request failed for some reason
                if(client.TryLogout())
                {
                    Message = $"Details: Logged out Successfully {client.RequestInterceptor}";
                    _logger.LogInformation(message:Message);
                }
                else
                {
                    Message = $"Details: Error {client.RequestInterceptor} while logging out";
                    _logger.LogError(Message);
                }
            }
        }
        // set Model to current result set salesOrder_app for display
        if(ModelState.IsValid)
        {
            salesOrders_App = salesOrder_app;
            if(salesOrders_App is null)
            {
                Message = $"Details: No SalesOrders found in the cache!";
                _logger.LogError(Message);
                throw new NullReferenceException(nameof(salesOrders_App));
            }
        }
        SetParms();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Message = $"Details: From Date is {FromDate}: To Date is {ToDate}";
        _logger.LogInformation(Message);

        salesOrders_App = await _context.SalesOrders.Where(x => x.Date >= FromDate && x.Date <= ToDate).ToListAsync();

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
            Message = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(Parms));
        }

        FromDate = (DateTime)Parms[0];
        ToDate = (DateTime)Parms[1];
        NumRecords = Convert.ToInt32(Parms[2]);
        Selected_SalesOrder_Type = (string)Parms[3];

        Message = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
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
            Message = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(Message);
            throw new NullReferenceException(nameof(Parms));
        }

        refreshFlag = false;

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["EditFlag"] = false;
        TempData["DeleteFlag"] = false;

        Message = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                  $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(Message);

        return;
    }
    
    #endregion
}