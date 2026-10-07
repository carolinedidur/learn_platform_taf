using LP.Selenium.TAF.Core.UI.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class CareersGeneralPage(IWebDriverWrapper driver)
    : BasePage(driver)
{
    private static readonly By _startSearchButtonLocator = By.CssSelector(".pinned-button .button-body");

    public CareersSearchPage OpenCareersSearchPage()
    {
        var startSearchButton = Driver.WaitUntilInteractable(_startSearchButtonLocator);
        startSearchButton.Click();

        return new CareersSearchPage(Driver);
    }
}
