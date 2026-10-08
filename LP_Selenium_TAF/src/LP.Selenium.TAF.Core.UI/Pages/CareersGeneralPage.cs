using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class CareersGeneralPage(IWebDriverWrapper driver, ILogger logger)
    : BasePage(driver, logger)
{
    private static readonly By _startSearchButtonLocator = By.CssSelector(".pinned-button .button-body");

    public CareersSearchPage OpenCareersSearchPage()
    {
        Logger.Info("Opening careers search page");

        var startSearchButton = Driver.WaitUntilInteractable(_startSearchButtonLocator);
        startSearchButton.Click();

        return new CareersSearchPage(Driver, Logger);
    }
}
