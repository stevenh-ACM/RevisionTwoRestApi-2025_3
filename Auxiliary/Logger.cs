#nullable disable

namespace RevisionTwoApp.RestApi.Auxiliary;

#region RequestLogger
/// <summary>
/// Provides methods to log HTTP requests and responses to a file.
/// </summary>
public static class RequestLogger
{
    private const string _requestsLogPath = "RequestsLog.txt";

    /// <summary>
    /// Logs the details of an HTTP response to the RequestsLog.txt file.
    /// </summary>
    /// <param name="responseMessage">The HTTP response message to log.</param>
    public static void LogResponse(HttpResponseMessage responseMessage)
    {
        try
        {
            using (var writer = new StreamWriter(_requestsLogPath,true))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine("Response");
                writer.WriteLine("\tStatus code: " + responseMessage.StatusCode);
                writer.WriteLine("\tContent: " + responseMessage?.Content.ReadAsStringAsync().Result);
                writer.WriteLine("-----------------------------------------");
                writer.WriteLine();
                writer.Flush();
                writer.Close();
            }
        }
        catch { }
    }

    /// <summary>
    /// Logs the details of an HTTP request to the RequestsLog.txt file.
    /// </summary>
    /// <param name="request">The HTTP request message to log.</param>
    public static void LogRequest(HttpRequestMessage request)
    {
        try
        {
            using var writer = new StreamWriter(_requestsLogPath,true);
            writer.WriteLine(DateTime.Now.ToString());
            writer.WriteLine("Request");
            writer.WriteLine("\tMethod: " + request.Method);
            string body = request.Content?.ReadAsStringAsync().Result;

            writer.WriteLine("\tURL: " + request.RequestUri);
            if (!String.IsNullOrEmpty(body))
                writer.WriteLine("\tBody: " + body);
            writer.WriteLine("-----------------------------------------");
            writer.WriteLine();
            writer.Flush();
            writer.Close();
        }
        catch { }
    }
}
#endregion