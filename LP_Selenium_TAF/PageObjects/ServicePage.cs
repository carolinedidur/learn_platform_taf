using OpenQA.Selenium;
using WebDriverManager.DriverWrapper;

namespace PageObjects;

public class ServicePage(IWebDriverWrapper driver) : BasePage(driver)
{
    private static readonly By _titleLocator = By.XPath("//div[contains(@class,'section__wrapper')]//span[contains(@class,'text-gradient') " +
        "and not(ancestor::*[contains(@class,'background-video')])]");
    private static readonly By _ourRelatedExpertiseSectionLocator = By.XPath("//span/span[contains(normalize-space(),'Our Related Expertise')]");

    public string GetTitle()
    {
        var title = Driver.WaitUntilVisible(_titleLocator).Text;
        return title;
    }

    public bool IsOurRelatedExpertiseSectionDisplayed()
    {
        var ourRelatedExpertiseSection = Driver.WaitUntilVisible(_ourRelatedExpertiseSectionLocator);
        return WebDriverWrapper.IsDisplayed(ourRelatedExpertiseSection);
    }
}
