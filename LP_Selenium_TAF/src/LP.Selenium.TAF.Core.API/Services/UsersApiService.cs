using LP.Selenium.TAF.Core.API.Builder;
using LP.Selenium.TAF.Core.API.Client;
using LP.Selenium.TAF.Core.API.Models;
using LP.Selenium.TAF.Core.Logging;
using RestSharp;

namespace LP.Selenium.TAF.Core.API.Services;

public class UsersApiService(IApiClient apiClient)
{
    private readonly IApiClient _client =
        apiClient ?? throw new ArgumentNullException(nameof(apiClient));

    private static ILogger Logger => Core.Logging.Logger.Instance;

    private static readonly string _usersEndpoint = "users";
    private static readonly string _invalidEndpoint = "invalidendpoint";

    public async Task<RestResponse<IEnumerable<User>>> GetUsersAsync()
    {
        Logger.Info("Sending GET request to retrieve users.");

        try
        {
            var request = new ApiRequestBuilder(_usersEndpoint, Method.Get)
                .AddHeader("Accept", "application/json")
                .Build();

            var response = await _client.ExecuteAsync<IEnumerable<User>>(request);

            LogResponse("GET", _usersEndpoint, response);

            return response;
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to retrieve users.", ex);
            throw;
        }
    }

    public async Task<RestResponse<CreatedUser>> CreateUserAsync(
        string name,
        string username)
    {
        Logger.Info($"Sending POST request to create user {username}.");

        try
        {
            var body = new { name, username };

            var request = new ApiRequestBuilder(_usersEndpoint, Method.Post)
                .AddHeader("Accept", "application/json")
                .AddJsonBody(body)
                .Build();

            var response =
                await _client.ExecuteAsync<CreatedUser>(request);

            LogResponse("POST", _usersEndpoint, response);

            return response;
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to create user {username}.", ex);
            throw;
        }
    }

    public async Task<RestResponse<object>> GetInvalidEndpointAsync()
    {
        Logger.Info("Sending GET request to an invalid endpoint.");

        try
        {
            var request = new ApiRequestBuilder(
                    _invalidEndpoint, Method.Get)
                .AddHeader("Accept", "application/json")
                .Build();

            var response =
                await _client.ExecuteAsync<object>(request);

            LogResponse("GET", _invalidEndpoint, response);

            return response;
        }
        catch (Exception ex)
        {
            Logger.Error("Request to invalid endpoint failed to execute.", ex);
            throw;
        }
    }

    private static void LogResponse<T>(
        string method,
        string endpoint,
        RestResponse<T> response)
    {
        var statusCode = (int)response.StatusCode;

        if (response.IsSuccessful)
        {
            Logger.Info($"{method} {endpoint} completed with HTTP {statusCode}.");
        }
        else
        {
            Logger.Warning(
                $"{method} {endpoint} returned HTTP {statusCode}. " +
                $"Error: {response.ErrorMessage}");
        }
    }
}
