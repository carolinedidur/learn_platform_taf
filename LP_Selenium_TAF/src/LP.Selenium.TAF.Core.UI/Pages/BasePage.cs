using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class BasePage
{
    private static readonly By _footerLocator = By.TagName("footer");
    private static readonly By _acceptAllCookiesButtonLocator = By.XPath("//button[normalize-space()='Accept All']");
    private static readonly By _cookiesBannerLocator = By.CssSelector("div[aria-label='Cookie banner']");

    protected BasePage(IWebDriverWrapper driver)
    {
        ArgumentNullException.ThrowIfNull(driver);
        Driver = driver;
    }

    protected IWebDriverWrapper Driver { get; }

    public void ScrollFooterIntoView()
    {
        var footer = Driver.WaitUntilVisible(_footerLocator);

        Driver.ScrollElementIntoView(footer);
    }

    public void DownloadFooterItem(string itemTitle)
    {
        var itemDownloadLink = Driver.WaitUntilInteractable(By.PartialLinkText(itemTitle.ToUpper()));
        itemDownloadLink.Click();
    }

    public string WaitForDownload(string expectedFileName, TimeSpan timeout)
    {
        return Driver.WaitForDownload(expectedFileName, timeout);
    }

    public void AcceptCookies()
    {
        var acceptAllCookiesButton = Driver.WaitUntilInteractable(_acceptAllCookiesButtonLocator);
        acceptAllCookiesButton.Click();
    }

    public void WaitForCookiesBannerToDisappear()
    {
        Driver.Wait.Until(d => !d.FindElements(_cookiesBannerLocator).Any(e => e.Displayed));
    }
}
