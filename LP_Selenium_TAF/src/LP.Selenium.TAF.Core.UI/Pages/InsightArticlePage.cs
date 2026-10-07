using LP.Selenium.TAF.Core.UI.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class InsightArticlePage(IWebDriverWrapper driver)
    : BasePage(driver)
{
    private static readonly By _titleLocator = By.TagName("h1");

    public string GetArticleTitle()
    {
        var title = Driver.WaitUntilVisible(_titleLocator);
        return title.Text;
    }
}
