using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.Driver;

namespace WebDriverManager.DriverWrapper;

public class WebDriverWrapper(IDriverManager driverManager, TimeSpan timeout) : IWebDriverWrapper
{
    private readonly IDriverManager _driverManager = driverManager;
    private readonly TimeSpan _timeout = timeout;

    public WebDriverWait Wait => new(_driverManager.Driver, _timeout);

    public IWebElement WaitUntilVisible(By locator) =>
       Wait.Until(d =>
       {
           IWebElement element = d.FindElement(locator);
           return element.Displayed ? element : null;
       })!;

    public IWebElement WaitUntilInteractable(By locator) =>
        Wait.Until(d =>
        {
            IWebElement element = d.FindElement(locator);
            return element.Displayed && element.Enabled ? element : null;
        })!;

    public void NavigateTo(string url)
    {
        _driverManager.Driver.Navigate().GoToUrl(url);
    }
}
