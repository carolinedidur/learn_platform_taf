using Microsoft.Extensions.Configuration;

namespace LP.Selenium.TAF.Core.Configuration;

public class ConfigurationService
{
    public ConfigurationService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        Configuration = configuration.Get<AppConfig>()
            ?? throw new InvalidOperationException("Failed to load application configuration.");
    }

    public AppConfig Configuration { get; }
}
