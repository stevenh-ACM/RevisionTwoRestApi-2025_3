#nullable disable

namespace RevisionTwoApp.RestApi.Auxiliary;

#region Site_Credential
/// <summary>
/// Returns the site credential to be used for the connection
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class Site_Credential(AppDbContext context, ILogger<object> logger )
{
    #region ctor
    private readonly AppDbContext  _context = context;
    private readonly ILogger<object> _logger = logger;
    #endregion

    #region properties
    /// <summary>
    /// List of site credentials retrieved from the database.
    /// </summary>
    public List<Credential> SiteCredentials = [];
    #endregion

    #region methods
    /// <summary>
    /// Find the selected site credential from the credentials dbset in the database
    /// </summary>
    /// <returns>Credential SiteCredential</returns>
    public async Task<Credential> GetSiteCredential()
    {
        SiteCredentials = await _context.Credentials.ToListAsync();

        if(SiteCredentials is null)
        {
            _logger.LogError("GetConnectionCredential: No Credentials exist. Please create at least one Credential!");
            return null;
        }

        // Get the Id of the selected site credential
        int Id = GetSiteId(SiteCredentials);

        if (Id < 0)
        {
            _logger.LogError($@"GetConnectionCredential: No Credentials exist, Id is {Id}. Please create at least one Credential!");
            return null;
        }
        else
        {
            // Using the id, get the credential from the dbset   
            Credential SiteCredential = SiteCredentials.FirstOrDefault(c => c.Id == Id);

            if(SiteCredential is null)
            {
                _logger.LogError($@"GetConnectionCredential: No Credentials exist, Id is {Id}. Please create at least one Credential!");
                return null;
            }
            else
            {
                return SiteCredential;
            }
        }
    }

    /// <summary>
    /// etermines the Id for the site credential checked in the credentials dbset in the database
    /// </summary>
    /// <param name="Credentials"></param>
    /// <returns>int Id</returns>
    private int GetSiteId(List<Credential> Credentials)
    {
        try
        {
            if(Credentials is null)
            {
                _logger.LogError("GetConnectionCredentials: No Credentials exist. Please create at least one Credential!");
                return -1;
            }
            else
            {
                foreach(Credential credential in Credentials)
                {
                    if(credential.IsChecked)
                    {
                        return credential.Id;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($@"Details: Exception in getAuthId: {ex.Message}");
            return -1;
        }
        return -1;
    }
    #endregion
}
#endregion