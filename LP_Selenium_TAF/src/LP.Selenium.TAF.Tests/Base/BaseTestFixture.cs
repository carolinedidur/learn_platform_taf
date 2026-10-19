using LP.Selenium.TAF.Core.Configuration;
using LP.Selenium.TAF.Core.Logging;

namespace LP.Selenium.TAF.Tests.Base;

public class BaseTestFixture
{
    public static ILogger Logger => Core.Logging.Logger.Instance;

    public static IConfigurationService ConfigurationService { get; } = new ConfigurationService();

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        Core.Logging.Logger.Initialize(ConfigurationService);
    }
}
