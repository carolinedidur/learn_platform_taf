using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class InsightsPage(IWebDriverWrapper driver, ILogger logger)
    : BasePage(driver, logger)
{
    private static readonly By _rightNavigationArrowLocator = By.XPath("//div[@data-configuration='single-full-width']//button[contains(@class,'slider__right-arrow')]");
    private static readonly By _activeSlideLocator = By.XPath("//div[@data-configuration='single-full-width']//div[contains(@class,'owl-item') and contains(@class,'active')]");
    private static readonly By _activeSlideArticleLinkLocator = By.TagName("a");
    private static readonly By _textWrapperLocator = By.CssSelector("p > span");

    public void SwipeCarousel(int numberOfSwipes)
    {
        Logger.Info($"Swiping carousel {numberOfSwipes} time(s)");

        for (var i = 0; i < numberOfSwipes; i++)
        {
            var rightNavigationArrow = Driver.WaitUntilInteractable(_rightNavigationArrowLocator);
            rightNavigationArrow.Click();
        }
    }

    public string GetActiveSlideTitle()
    {
        Logger.Info("Getting active slide");

        var activeSlide = Driver.WaitUntilVisible(_activeSlideLocator);
        var titleWrapper = activeSlide.FindElement(_textWrapperLocator);

        return titleWrapper.Text;
    }

    public InsightArticlePage OpenActiveSlideArticle()
    {
        Logger.Info("Opening current slide's article");

        var activeSlide = Driver.WaitUntilVisible(_activeSlideLocator);
        var articleLink = activeSlide.FindElement(_activeSlideArticleLinkLocator);

        articleLink.Click();

        return new InsightArticlePage(Driver, Logger);
    }
}
