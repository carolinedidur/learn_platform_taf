using LP.Selenium.TAF.Core.API.Client;
using LP.Selenium.TAF.Core.Configuration;
using RestSharp;

namespace LP.Selenium.TAF.Core.API.Factories;

public static class ApiClientFactory
{
    public static ApiClient Create(IConfigurationService configurationService)
    {
        var baseUrl = configurationService.Configuration.ApiConfig.BaseUrl;
        var options = new RestClientOptions(baseUrl);
        var restClient = new RestClient(options);

        return new ApiClient(restClient);
    }
}
