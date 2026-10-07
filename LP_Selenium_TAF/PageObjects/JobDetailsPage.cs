using OpenQA.Selenium;
using WebDriverManager.DriverWrapper;

namespace PageObjects;

public class JobDetailsPage(IWebDriverWrapper driver) : BasePage(driver)
{
    private static readonly By _jobTitleLocator = By.CssSelector("h1[data-testid='job-details-banner-title']");

    public string GetJobTitle()
    {
        var jobTitle = Driver.WaitUntilVisible(_jobTitleLocator);

        return jobTitle.Text;
    }
}
