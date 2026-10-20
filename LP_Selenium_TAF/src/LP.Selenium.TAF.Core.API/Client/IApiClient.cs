using RestSharp;

namespace LP.Selenium.TAF.Core.API.Client;

public interface IApiClient
{
    public Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request);

    public Task<RestResponse<T>> GetAsync<T>(string endpoint);

    public Task<RestResponse<T>> PostAsync<T>(string endpoint, object body);
}
