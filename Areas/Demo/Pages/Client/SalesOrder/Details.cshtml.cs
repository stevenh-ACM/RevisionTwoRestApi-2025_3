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

using Newtonsoft.Json;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Models.App;
using Credential = RevisionTwoApp.RestApi.Models.Credential;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1572 // XML comment has badly formed XML

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Client.SalesOrder;

#region DetailsModel
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
    public List<Acumatica.Default_24_200_001.Model.SalesOrder> salesOrders { get; set; } // salesOrder Acumatica ERP model

    /// <summary>
    /// Gets or sets the list of sales orders retrieved from the database or API.
    /// </summary>
    public List<SalesOrder_App> SalesOrders { get; set; } = []; // SalesOrders local store table

    /// <summary>
    /// Gets or sets the collection of sales order shipments associated with the current sales order.
    /// </summary>
    public List<SalesOrderShipment> SalesOrderShipments { get; set; } = [];

    /// <summary>
    /// Gets or sets the site credentials object used to hold credentials for Acumatica ERP connection.
    /// </summary>
    public Site_Credential SiteCredential { get; set; } // site object to hold credentials for Acumatica ERP connection

    /// <summary>
    /// Gets or sets the credentials used for authentication.
    /// </summary>
    public Credential credential { get; set; }

    /// <summary>
    /// Parameters used for values passing and flags.
    /// </summary>
    public List<object> Parms { get; set; } = [new()];
    public Boolean RefreshFlag { get; set; } = true; // is the page simply being refreshed
    public string Selected_SalesOrder_Type { get; set; }
    public int NumRecords { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime FromDate { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime ToDate { get; set; }
    #endregion

    #region methods
    /// <summary>
    /// Handles the GET request for the page, managing the retrieval and synchronization of sales order data.
    /// </summary>
    /// <remarks>This method performs several operations based on the state of the application: <list
    /// type="bullet"> <item> If the TempData flags <c>EditFlag</c> or <c>DeleteFlag</c> are set to <see
    /// langword="true"/>, it refreshes the local cache of sales orders. </item> <item> If the local store requires
    /// refreshing, indicated by the <c>RefreshFlag</c>, it retrieves sales order data from an external Acumatica ERP
    /// system and updates the local database. </item> <item> If no refresh is required, it clears the local store and
    /// prepares it for future updates. </item> </list> The method ensures that the local store is synchronized with the
    /// external system when necessary and handles exceptions related to data retrieval or connectivity
    /// issues.</remarks>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Typically, this is the current page,
    /// but it may redirect to another page if certain conditions are met.</returns>
    /// <exception cref="NullReferenceException">Thrown if critical objects such as <c>SalesOrders</c> or the API client cannot be initialized.</exception>
    /// <exception cref="Exception">Thrown if an error occurs during the interaction with the external Acumatica ERP system.</exception>
    public async Task<IActionResult> OnGetAsync()
    {
        GetParms(); //retrieves parameters and flags for the SalesOrder

        if ((bool)TempData["EditFlag"] || (bool)TempData["DeleteFlag"])
        {
            // refresh cache with any changes made in Edit or Delete
            SalesOrders = await _context.SalesOrders.ToListAsync();
            if(SalesOrders is null)
            {
                var errorMessage = $"Details: No SalesOrders found in the cache!";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(SalesOrders));
            }

            if(SalesOrders.Count == 0)
            {
                var errorMessage = $"Details: No SalesOrders found in the cache!";
                _logger.LogError(errorMessage);

                return RedirectToPage("/Index");
            }

            // refreshFlag set to true when local store has been refreshed
            RefreshFlag = false;
        }
        else
        {
            // clear salesOrders in local store
            _context.SalesOrders.RemoveRange();
            _context.SaveChanges();

            // refreshFlag set to false when local store needs refreshing
            RefreshFlag = true;
        }

        if (RefreshFlag) // RefreshFlag true means to retrieve records from Acumatica ERP and replace local store
        {
            SiteCredential = new(_context, _logger);

            credential = SiteCredential.GetSiteCredential().Result;

            // httpClient RestClient created with baseURL assigned
            var client = new ApiClient(credential.SiteUrl,
                                               requestInterceptor: RequestLogger.LogRequest,
                                               responseInterceptor: RequestLogger.LogResponse,
                                               ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                              );
            if(client is null)
            {
                var errorMessage = $"Details: Failure to create an client context. SiteURL is {credential.SiteUrl}";
                _logger.LogError(message: errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                var infoMessage = $"Details: Created client context with SiteURL of {credential.SiteUrl}";
                _logger.LogInformation(infoMessage);
            }

            // log in to Acumatica ERP using credentials with logout finally block
            try
            {
                //RestClient Log In (on) using Credentials retrieved
                client.Login(credential.UserName,credential.Password,"","","");

                if(client.RequestInterceptor is null)
                {
                    var errorMessage = $"Details: Failure to create an configuration context. client login has UserName of " +
                                            $"{credential.UserName} and Password of {credential.Password}";
                    _logger.LogError(errorMessage);
                    throw new NullReferenceException(nameof(client));
                }
                else
                {
                     var infoMessage = "Details: Reading Accounts for parameters chosen";
                    _logger.LogInformation(infoMessage);
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

                DateTimeValue defaultDate = DateTime.Parse("2020-01-01T00:00:01.000");

                for(int idx = 0;idx < (salesOrders.Count);idx++)
                {
                    if(salesOrders[idx].Shipments.Count == 0)
                    {
                        SalesOrderShipments.Add(new SalesOrderShipment());
                        salesOrders[idx].Shipments.AddRange(SalesOrderShipments);
                        salesOrders[idx].Shipments[0].ShipmentDate = defaultDate;
                    }

                    string[] customerID = [salesOrders[idx].CustomerID];

                    var baAccount = client.GetByKeys<BusinessAccount>(customerID);
                    if(baAccount is null)
                    {
                        var errorMessage = $"Details: Failure to create a salesOrderApi using {client.ToString}";
                        _logger.LogError(errorMessage);

                        throw new NullReferenceException(nameof(baAccount));
                    }

                    //Create an data model using field values from Api calls
                    var _so = new ConvertToSO(salesOrders[idx],baAccount,salesOrders[idx].Shipments[0].ShipmentDate);

                    //Add salesOrder to local store
                    await _context.SalesOrders.AddAsync(_so);
                }
                //Write list of records accumulated to local store dB
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                var errorMessage = $"Details: Exception caught {e.Message}";
                _logger.LogError(errorMessage);

                throw new Exception(nameof(client), e);
            }
            finally
            {
                //we use logout in finally block because we need to always log out, even if the request failed for some reason
                if(client.TryLogout())
                {
                    var infoMessage = $"Details: Logged out Successfully {client.RequestInterceptor}";
                    _logger.LogInformation(infoMessage);
                }
                else
                {
                    var errorMessage = $"Details: Error {client.RequestInterceptor} while logging out";
                    _logger.LogError(errorMessage);
                }
            }
        }

        SalesOrders = await _context.SalesOrders.ToListAsync(); //retrieve the latest salesOrders from local store

        var infoMessage2 = $@"Details: No need to retrieve records from Acumatica ERP, RefreshFlag is {RefreshFlag}";
        _logger.LogInformation(infoMessage2);

        SetParms(); //resets parameters and false for the SalesOrder

        return Page();
    }

    /// <summary>
    /// Handles the POST request for the Details page.
    /// Retrieves sales orders filtered by the specified date range and updates the model.
    /// </summary>
    /// <returns>An IActionResult representing the result of the POST operation.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var infoMessage = $"Details: From Date is {FromDate}: To Date is {ToDate}";
        _logger.LogInformation(infoMessage);

        SalesOrders = await _context.SalesOrders.Where(x => x.Date >= FromDate && x.Date <= ToDate).ToListAsync();

        SetParms();

        return Page();
    }
    #endregion

    #region private methods
    private void GetParms()
    {
        Parms = JsonConvert.DeserializeObject<List<object>>((string)TempData["parms"]);
        if(Parms is null)
        {
            var errorMessage = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(errorMessage);
            throw new NullReferenceException(nameof(Parms));
        }

        FromDate = (DateTime)Parms[0];
        ToDate = (DateTime)Parms[1];
        NumRecords = Convert.ToInt32(Parms[2]);
        Selected_SalesOrder_Type = (string)Parms[3];

        var infoMessage = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
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
            var errorMessage = $"Details: No parameters exist. Please check your parameters!";
            _logger.LogError(errorMessage);
            throw new NullReferenceException(nameof(Parms));
        }

        RefreshFlag = false;

        TempData["parms"] = JsonConvert.SerializeObject(Parms);
        TempData["EditFlag"] = false;
        TempData["DeleteFlag"] = false;

        var infoMessage = $"Details: Set Parameters assigned: FromDate: {FromDate}, ToDate: {ToDate}, " +
                                $"NumRecords: {NumRecords}, OrderType: {Selected_SalesOrder_Type}";
        _logger.LogInformation(infoMessage);
    }
    #endregion
}
#endregion