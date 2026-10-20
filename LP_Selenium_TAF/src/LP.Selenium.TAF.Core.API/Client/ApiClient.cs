using RestSharp;

namespace LP.Selenium.TAF.Core.API.Client;

public class ApiClient(RestClient client)
    : IApiClient
{
    private readonly RestClient _client = client ?? throw new ArgumentNullException(nameof(client));

    public Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
    {
        return _client.ExecuteAsync<T>(request);
    }

    public Task<RestResponse<T>> GetAsync<T>(string endpoint)
    {
        return _client.ExecuteAsync<T>(new RestRequest(endpoint, Method.Get));
    }

    public Task<RestResponse<T>> PostAsync<T>(string endpoint, object body)
    {
        return _client.ExecuteAsync<T>(new RestRequest(endpoint, Method.Post).AddJsonBody(body));
    }
}
