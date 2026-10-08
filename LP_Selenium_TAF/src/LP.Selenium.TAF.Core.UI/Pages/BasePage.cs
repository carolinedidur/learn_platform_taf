using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class BasePage
{
    private static readonly By _footerLocator = By.TagName("footer");
    private static readonly By _acceptAllCookiesButtonLocator = By.XPath("//button[normalize-space()='Accept All']");
    private static readonly By _cookiesBannerLocator = By.CssSelector("div[aria-label='Cookie banner']");

    protected BasePage(IWebDriverWrapper driver, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(driver);
        Driver = driver;
        Logger = logger;
    }

    protected IWebDriverWrapper Driver { get; }

    protected ILogger Logger { get; }

    public void ScrollFooterIntoView()
    {
        Logger.Info("Scrolling footer into view");

        var footer = Driver.WaitUntilVisible(_footerLocator);
        Driver.ScrollElementIntoView(footer);
    }

    public void DownloadFooterItem(string itemTitle)
    {
        Logger.Info($"Clicking download link for {itemTitle}");

        var itemDownloadLink = Driver.WaitUntilInteractable(By.PartialLinkText(itemTitle.ToUpper()));
        itemDownloadLink.Click();
    }

    public string WaitForDownload(string expectedFileName, TimeSpan timeout)
    {
        Logger.Info($"Waiting for download: {expectedFileName}");

        return Driver.WaitForDownload(expectedFileName, timeout);
    }

    public void AcceptCookies()
    {
        Logger.Info("Accepting cookies");

        var acceptAllCookiesButton = Driver.WaitUntilInteractable(_acceptAllCookiesButtonLocator);
        acceptAllCookiesButton.Click();
    }

    public void WaitForCookiesBannerToDisappear()
    {
        Logger.Info("Waiting for cookies banner to disappear");

        Driver.Wait.Until(d => !d.FindElements(_cookiesBannerLocator).Any(e => e.Displayed));
    }
}
