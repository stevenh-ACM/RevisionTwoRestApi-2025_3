#nullable disable

using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Auxiliary;

public class AuthId
{
    public int getAuthId(List<Credential> credentials)
    {
        foreach(Credential credential in credentials)
        {
            if(credential.IsChecked)
            {
                return credential.Id;
            }
        }
        return 0;
    }
}
