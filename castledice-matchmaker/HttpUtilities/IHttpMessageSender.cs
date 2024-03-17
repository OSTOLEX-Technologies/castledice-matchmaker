namespace castledice_matchmaker.HttpUtilities;

public interface IHttpMessageSender
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}