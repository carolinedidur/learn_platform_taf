using LP.Selenium.TAF.Core.Configuration;
using LP.Selenium.TAF.Core.Enums;
using LP.Selenium.TAF.Core.UI.Factories;
using LP.Selenium.TAF.Core.UI.Services.Download;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Driver.DriverManager;

public sealed class DriverManager(IConfigurationService configurationService)
    : IDriverManager
{
    private readonly IConfigurationService _configuration = configurationService;
    private IWebDriver? _driver;

    public IWebDriver Driver => _driver ?? throw new InvalidOperationException("Browser is not started.");

    public IDownloadService DownloadService { get; private set; } = null!;

    public void StartBrowser()
    {
        DownloadService = new DownloadService();

        if (!Enum.TryParse<BrowserType>(_configuration.Configuration.Driver.Browser, true, out var browserType))
        {
            throw new ArgumentException($"Unsupported browser: {_configuration.Configuration.Driver.Browser}");
        }

        _driver = DriverFactory.Create(browserType, DownloadService.Directory, _configuration.Configuration.Driver.Headless);
        _driver.Manage().Window.Maximize();
    }

    public void QuitBrowser()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
    }
}
