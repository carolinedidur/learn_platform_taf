using LP.Selenium.TAF.Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace LP.Selenium.TAF.Core.UI.Factories;

public static class DriverFactory
{
    public static IWebDriver Create(BrowserType browser, string downloadDirectory, bool headless = false)
    {
        return browser switch
        {
            BrowserType.Chrome => CreateChrome(downloadDirectory, headless),
            BrowserType.Firefox => CreateFirefox(downloadDirectory, headless),
            BrowserType.Edge => CreateEdge(downloadDirectory, headless),
            _ => throw new ArgumentException($"Unsupported browser: {browser}")
        };
    }

    private static IWebDriver CreateChrome(string downloadDirectory, bool headless)
    {
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
        }

        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefox(string downloadDirectory, bool headless)
    {
        var options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("-headless");
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
        }

        options.SetPreference("browser.download.folderList", 2);
        options.SetPreference("browser.download.dir", downloadDirectory);
        options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf,application/octet-stream");
        options.SetPreference("pdfjs.disabled", true);

        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdge(string downloadDirectory, bool headless)
    {
        var options = new EdgeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
        }

        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);

        return new EdgeDriver(options);
    }
}
