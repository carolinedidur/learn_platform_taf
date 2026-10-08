using LP.Selenium.TAF.Core.Configuration;
using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverManager;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using LP.Selenium.TAF.Core.UI.Services.Screenshots;
using NUnit.Framework.Interfaces;

namespace LP.Selenium.TAF.Tests.Base;

public abstract class BaseTest
{
    public static ILogger Logger => Core.Logging.Logger.Instance;

    public IDriverManager DriverManager { get; private set; }

    public IWebDriverWrapper Driver { get; private set; }

    private static IConfigurationService ConfigurationService { get; } = new ConfigurationService();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Core.Logging.Logger.Initialize(ConfigurationService);
    }

    [SetUp]
    public void SetUp()
    {
        Logger.Info($"Test started: {TestContext.CurrentContext.Test.Name}");

        DriverManager = new DriverManager(ConfigurationService);

        var timeout = TimeSpan.FromSeconds(ConfigurationService.Configuration.Driver.Timeout);
        Driver = new WebDriverWrapper(DriverManager, timeout);

        Logger.Info($"Browser started: {ConfigurationService.Configuration.Driver.Browser}, headless={ConfigurationService.Configuration.Driver.Headless}");

        DriverManager.StartBrowser();
        Driver.NavigateTo(ConfigurationService.Configuration.Environment.BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            var result = TestContext.CurrentContext.Result;

            if (result.Outcome.Status == TestStatus.Failed)
            {
                Logger.Error($"Test failed: {result.Message}");
                TryTakeScreenshot();
            }
            else
            {
                Logger.Info($"Test finished: {result.Outcome.Status}");
            }
        }
        finally
        {
            DriverManager.QuitBrowser();
            Logger.Info("Browser closed");
        }
    }

    private void TryTakeScreenshot()
    {
        try
        {
            var path = ScreenshotService.TakeScreenshot(DriverManager, TestContext.CurrentContext.Test.Name);
            TestContext.AddTestAttachment(path);
            Logger.Info($"Screenshot saved at: {path}");
        }
        catch (Exception ex)
        {
            Logger.Error("Could not take a screenshot", ex);
        }
    }
}
