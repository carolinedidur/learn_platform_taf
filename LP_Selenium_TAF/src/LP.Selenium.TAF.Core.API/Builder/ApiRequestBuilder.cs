using RestSharp;

namespace LP.Selenium.TAF.Core.API.Builder;

public class ApiRequestBuilder(string endpoint, Method method)
{
    private readonly RestRequest _request = new(endpoint, method);

    public ApiRequestBuilder AddHeader(string name, string value)
    {
        _request.AddHeader(name, value);
        return this;
    }

    public ApiRequestBuilder AddJsonBody(object body)
    {
        _request.AddJsonBody(body);
        return this;
    }

    public RestRequest Build()
    {
        return _request;
    }
}
