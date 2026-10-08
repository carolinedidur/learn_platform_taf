using Microsoft.Extensions.Configuration;

namespace LP.Selenium.TAF.Core.Configuration;

public class ConfigurationService : IConfigurationService
{
    public ConfigurationService()
    {
        var environment = Environment.GetEnvironmentVariable("TAF_ENV") ?? "Dev";

        var root = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables(prefix: "TAF_")
            .Build();

        Configuration = root.Get<AppConfig>()
            ?? throw new InvalidOperationException("Failed to load application configuration.");
    }

    public AppConfig Configuration { get; }
}
