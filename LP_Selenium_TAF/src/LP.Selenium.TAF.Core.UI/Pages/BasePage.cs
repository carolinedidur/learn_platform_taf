using LP.Selenium.TAF.Core.UI.DriverWrapper;
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

    public string WaitForDownload(string directory, string expectedFileName, TimeSpan timeout)
    {
        var expectedPath = Path.Combine(directory, expectedFileName);

        Driver.WaitUntilWithCustomTimeout(
            _ => File.Exists(expectedPath)
                            && !Directory.EnumerateFiles(directory, "*.crdownload").Any(),
            timeout,
            message: $"'{expectedFileName}' was not downloaded. Files in folder: [{string.Join(", ", Directory.EnumerateFiles(directory).Select(Path.GetFileName))}]");

        return expectedPath;
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
