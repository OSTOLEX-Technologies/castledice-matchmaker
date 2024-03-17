namespace castledice_matchmaker.HttpUtilities;

public class HttpClientWrapper : IHttpMessageSender
{
    private readonly HttpClient _client;

    public HttpClientWrapper(HttpClient client)
    {
        _client = client;
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return _client.SendAsync(request);
    }
}