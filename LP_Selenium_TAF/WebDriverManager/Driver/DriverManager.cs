using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        this._driver = new ChromeDriver(options);
        this._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
    }

    public void QuitBrowser()
    {
        this._driver?.Quit();
        this._driver?.Dispose();
        this._driver = null;
    }
}
