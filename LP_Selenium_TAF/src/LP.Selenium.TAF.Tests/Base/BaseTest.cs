using LP.Selenium.TAF.Core.Configuration;
using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverManager;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;

namespace LP.Selenium.TAF.Tests.Base;

public class BaseTest
{
    public IDriverManager DriverManager { get; private set; }

    public IWebDriverWrapper Driver { get; private set; }

    public ILogger Logger { get; private set; } = new Logger();

    private static IConfigurationService ConfigurationService { get; } = new ConfigurationService();

    [SetUp]
    public void SetUp()
    {
        DriverManager = new DriverManager(ConfigurationService);

        var timeout = TimeSpan.FromSeconds(ConfigurationService.Configuration.Driver.Timeout);
        Driver = new WebDriverWrapper(DriverManager, timeout);

        DriverManager.StartBrowser();
        Driver.NavigateTo(ConfigurationService.Configuration.Environment.BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        DriverManager.QuitBrowser();
    }
}
