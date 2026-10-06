using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverManager.Driver;

public sealed class DriverManager() : IDriverManager
{
    private IWebDriver? _driver;

    public IWebDriver Driver => _driver ?? throw new InvalidOperationException("Browser is not started.");

    public void StartBrowser()
    {
        var options = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Normal,
        };
        options.AddArgument("--disable-notifications");
        options.AddArgument("--start-maximized");

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
    }

    public void QuitBrowser()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
    }
}
