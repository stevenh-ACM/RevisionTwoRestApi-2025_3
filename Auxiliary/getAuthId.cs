#nullable disable

using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Auxiliary;

/// <summary>
/// Provides methods to retrieve authentication-related information.
/// </summary>
public class AuthId
{
    /// <summary>
    /// Retrieves the ID of the first checked credential from the provided list.
    /// </summary>
    /// <param name="credentials">A list of credentials to search through.</param>
    /// <returns>The ID of the first checked credential, or 0 if none are checked.</returns>
    public int getAuthId(List<Credential> credentials)
    {
        foreach (Credential credential in credentials)
        {
            if (credential.IsChecked)
            {
                return credential.Id;
            }
        }
        return 0;
    }
}
