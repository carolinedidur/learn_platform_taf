using LP.Selenium.TAF.Core.UI.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class InsightsPage(IWebDriverWrapper driver)
    : BasePage(driver)
{
    private static readonly By _rightNavigationArrowLocator = By.XPath("//div[@data-configuration='single-full-width']//button[contains(@class,'slider__right-arrow')]");
    private static readonly By _activeSlideLocator = By.XPath("//div[@data-configuration='single-full-width']//div[contains(@class,'owl-item') and contains(@class,'active')]");
    private static readonly By _activeSlideArticleLinkLocator = By.TagName("a");
    private static readonly By _textWrapperLocator = By.CssSelector("p > span");

    public void SwipeCarousel(int numberOfSwipes)
    {
        for (var i = 0; i < numberOfSwipes; i++)
        {
            var rightNavigationArrow = Driver.WaitUntilInteractable(_rightNavigationArrowLocator);
            rightNavigationArrow.Click();
        }
    }

    public string GetActiveSlideTitle()
    {
        var activeSlide = Driver.WaitUntilVisible(_activeSlideLocator);
        var titleWrapper = activeSlide.FindElement(_textWrapperLocator);

        return titleWrapper.Text;
    }

    public InsightArticlePage OpenActiveSlideArticle()
    {
        var activeSlide = Driver.WaitUntilVisible(_activeSlideLocator);
        var articleLink = activeSlide.FindElement(_activeSlideArticleLinkLocator);

        articleLink.Click();

        return new InsightArticlePage(Driver);
    }
}
