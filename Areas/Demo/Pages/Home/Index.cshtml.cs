#nullable disable

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Auxiliary;
using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.DTOs.Conversions;
using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region IndexModel
/// <summary>
/// Represents the model for the Index page in the Home area of the Demo section.
/// </summary>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    public readonly ILogger<IndexModel> _logger = logger;
    public readonly AppDbContext _context = context;
    #endregion

    #region properties
    /// <summary>
    /// The URL of the site.
    /// </summary>
    [BindProperty]
    public string SiteUrl { get; set; } = default;

    /// <summary>
    /// Object to hold credentials for Acumatica ERP connection.
    /// </summary>
    public Site_Credential SiteCredential { get; set; }

    /// <summary>
    /// Gets or sets the credential used for authentication or authorization.
    /// </summary>
    public Credential credential { get; set; }
    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests to the Index page.
    /// </summary>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    public IActionResult OnGet()
    {
        // Get the selected connection credentials to access Acumatica ERP
        SiteCredential = new Site_Credential(_context,_logger);

        credential = SiteCredential.GetSiteCredential().Result;

        if (credential is null)
        {
            var errorMessage = "Home/Index: No Credentials exist. Please create at least one Credential!";
            _logger.LogError(errorMessage);

            return RedirectToPage("Credentials/Create");
        }
        else
        {
            var infoMessage = $@"Home/Index: Credentials retrieved successfully. SiteURL is {credential.SiteUrl}";
            _logger.LogInformation(infoMessage);
        }

        // httpClient RestClient created with baseURL assigned
        var client = new ApiClient(
                                    credential.SiteUrl,
                                            requestInterceptor: RequestLogger.LogRequest,
                                            responseInterceptor: RequestLogger.LogResponse,
                                            ignoreSslErrors: true // this is here to allow testing with self-signed certificates
                                           );
        if (client is null)
        {
            var errorMessage = $"Index: Failure to create an client context. SiteURL is {credential.SiteUrl}";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(client));
        }

        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName, credential.Password, "", "", "");
            if (client.RequestInterceptor is null)
            {
                var errormessage = $"Home/IndexDetails: Failure to create an configuration context. client login has UserName of " +
                                         $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(errormessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                var infoMessage = "Index: Reading Customers for local store";
                _logger.LogInformation(infoMessage);
            }

            //Rest parameters for API methods

            //Invoke GetList (of Customers per parameters) on Customer Api
            var select = "CustomerID,CustomerName";
            var customers = client.GetList<Acumatica.Default_24_200_001.Model.Customer>(null, select, top:50);

            //add customers to local store
            for (int i = 0; i < customers.Count; i++)
            {
                var message = $"Home/Index: Customer {i + 1} of {customers.Count} is {customers[i].CustomerID} - {customers[i].CustomerName}";
                _logger.LogInformation(message);
                
                var _cu = new ConvertToCU(customers[i]);

                //Add Customer_App to local store
                _context.Customers.Add(_cu);
            }

            //Write list of records accumulated to dB
            _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            var Message = $"Home/Index: Exception caught {e.Message}";
            _logger.LogError(Message);
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                var Message = $"Home/Index: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(message: Message);
            }
            else
            {
                var Message = $"Home/Index: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(Message);
            }
        }

        return Page();
    }
    #endregion
}
#endregion