using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverManager.Driver;

public sealed class DriverManager() : IDriverManager
{
    private IWebDriver? _driver;

    public IWebDriver Driver => _driver ?? throw new InvalidOperationException("Browser is not started.");
    public string DownloadDirectory { get; private set; } = string.Empty;

    public void StartBrowser()
    {
        DownloadDirectory = Path.Combine(Path.GetTempPath(), "downloads", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(DownloadDirectory);

        var options = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Normal,
        };
        options.AddArgument("--disable-notifications");
        options.AddArgument("--start-maximized");

        options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);

        _driver = new ChromeDriver(options);
    }

    public void QuitBrowser()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
    }
}
