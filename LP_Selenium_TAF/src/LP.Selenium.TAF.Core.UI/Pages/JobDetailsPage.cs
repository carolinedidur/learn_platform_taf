using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class JobDetailsPage(IWebDriverWrapper driver, ILogger logger)
    : BasePage(driver, logger)
{
    private static readonly By _jobTitleLocator = By.CssSelector("h1[data-testid='job-details-banner-title']");

    public string GetJobTitle()
    {
        Logger.Info("Retrieving text from job title");

        var jobTitle = Driver.WaitUntilVisible(_jobTitleLocator);

        return jobTitle.Text;
    }
}
