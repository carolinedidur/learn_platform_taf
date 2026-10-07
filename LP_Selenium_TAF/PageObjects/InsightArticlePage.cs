using OpenQA.Selenium;
using WebDriverManager.DriverWrapper;

namespace PageObjects;

public class InsightArticlePage(IWebDriverWrapper driver) : BasePage(driver)
{
    private static readonly By _titleLocator = By.TagName("h1");

    public string GetArticleTitle()
    {
        var title = Driver.WaitUntilVisible(_titleLocator);
        return title.Text;
    }
}
