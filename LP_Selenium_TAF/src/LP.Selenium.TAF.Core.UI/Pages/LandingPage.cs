using LP.Selenium.TAF.Core.Logging;
using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class LandingPage(IWebDriverWrapper driver, ILogger logger)
    : BasePage(driver, logger)
{
    private static readonly By _topNavigationRowLocator = By.ClassName("top-navigation__row");
    private static readonly By _careersNavigationLinkLocator = By.LinkText("Careers");
    private static readonly By _insightsNavigationLinkLocator = By.LinkText("Insights");
    private static readonly By _searchFieldTextboxLocator = By.Name("q");
    private static readonly By _magnifierButtonLocator = By.CssSelector("button[class*='header-search__button']");
    private static readonly By _findButtonLocator = By.XPath("//button[descendant::span[@class='bth-text-layer']]");

    public CareersGeneralPage OpenCareersGeneralPage()
    {
        Logger.Info("Opening careers page");

        var topNavigationRow = Driver.WaitUntilInteractable(_topNavigationRowLocator);
        var careersNavigationLink = topNavigationRow.FindElement(_careersNavigationLinkLocator);
        careersNavigationLink.Click();

        return new CareersGeneralPage(Driver, Logger);
    }

    public InsightsPage OpenInsightsPage()
    {
        Logger.Info("Opening insights page");

        var topNavigationRow = Driver.WaitUntilInteractable(_topNavigationRowLocator);
        var insightsNavigationLink = topNavigationRow.FindElement(_insightsNavigationLinkLocator);
        insightsNavigationLink.Click();
        return new InsightsPage(Driver, Logger);
    }

    public SearchResultsPage SearchByKeyword(string keyword)
    {
        Logger.Info("Clicking magnifier icon button");

        var magnifierButton = Driver.WaitUntilInteractable(_magnifierButtonLocator);
        magnifierButton.Click();

        Logger.Info($"Entering search keyword: {keyword}");

        var searchFieldTextbox = Driver.WaitUntilInteractable(_searchFieldTextboxLocator);
        searchFieldTextbox.Clear();
        searchFieldTextbox.SendKeys(keyword);

        Logger.Info("Clicking find button");

        var findButton = Driver.WaitUntilInteractable(_findButtonLocator);
        findButton.Click();

        return new SearchResultsPage(Driver, Logger);
    }
}
