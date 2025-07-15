#nullable disable

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region IndexModel
/// <summary>
/// Represents the model for the Index page, providing functionality to handle GET requests and manage credentials for
/// connecting to Acumatica ERP.
/// </summary>
/// <remarks>This class is responsible for retrieving credentials, establishing a connection to Acumatica ERP, and
/// performing operations such as fetching customer data and storing it locally. It also handles logging for various
/// operations and ensures proper cleanup, such as logging out from the client.</remarks>
/// <param name="context"></param>
/// <param name="logger"></param>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<IndexModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _className = nameof(IndexModel);
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
            var errorMessage = $"{_className}: No Credentials exist. Please create at least one Credential!";
            _logger.LogError(errorMessage);

            return RedirectToPage("Credentials/Create");
        }
        else
        {
            var infoMessage = $@"{_className}: Credentials retrieved successfully. SiteURL is {credential.SiteUrl}";
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
            var errorMessage = $@"{_className}: Failure to create an client context. SiteURL is {credential.SiteUrl}";
            _logger.LogError(errorMessage);

            throw new NullReferenceException(nameof(client));
        }

        try
        {
            //RestClient Log In (on) using Credentials retrieved
            client.Login(credential.UserName, credential.Password, "", "", "");
            if (client.RequestInterceptor is null)
            {
                var errorMessage = $"{_className}: Failure to create an configuration context. client login has UserName of " +
                                         $"{credential.UserName} and Password of {credential.Password}";
                _logger.LogError(errorMessage);

                throw new NullReferenceException(nameof(client));
            }
            else
            {
                var infoMessage = $"{_className}: Reading Customers for local store";
                _logger.LogInformation(infoMessage);
            }

            //Rest parameters for API methods

            // Get list (of Customers per parameters) on Customer Api
            var select = "CustomerID,CustomerName";
            var customers = client.GetList<Acumatica.Default_24_200_001.Model.Customer>(null, select, top:50);

            //add customers to local store
            for (int i = 0; i < customers.Count; i++)
            {
                var infoMessage = $"{_className}: Customer {i + 1} of {customers.Count} is {customers[i].CustomerID} - {customers[i].CustomerName}";
                _logger.LogInformation(infoMessage);
                
                var _cu = new ConvertToCU(customers[i]);

                // Add customers to local store
                _context.Customers.Add(_cu);
            }

            //Write list of records accumulated to dB
            _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            var Message = $"{_className}: Exception caught {e.Message}";
            _logger.LogError(Message);
        }
        finally
        {
            //we use logout in finally block because we need to always log out, even if the request failed for some reason
            if (client.TryLogout())
            {
                var Message = $"{_className}: Logged out Successfully {client.RequestInterceptor}";
                _logger.LogInformation(message: Message);
            }
            else
            {
                var Message = $"{_className}: Error {client.RequestInterceptor} while logging out";
                _logger.LogError(Message);
            }
        }
        return Page();
    }
    #endregion
}
#endregion