using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverWrapper;

namespace PageObjects.Pages;

public class LandingPage(IWebDriverWrapper driver) : BasePage(driver)
{
    private static readonly By _topNavigationRowLocator = By.ClassName("top-navigation__row");
    private static readonly By _careersNavigationLinkLocator = By.LinkText("Careers");
    private static readonly By _insightsNavigationLinkLocator = By.LinkText("Insights");
    private static readonly By _searchFieldTexboxLocator = By.Name("q");
    private static readonly By _searchButtonLocator = By.CssSelector("button[class*='header-search__button']");
    private static readonly By _findButtonLocator = By.XPath("//button[descendant::span[@class='bth-text-layer']]");

    public CareersGeneralPage OpenCareersGeneralPage()
    {
        var topNavigationRow = Driver.WaitUntilInteractable(_topNavigationRowLocator);
        var careersNavigationLink = topNavigationRow.FindElement(_careersNavigationLinkLocator);
        careersNavigationLink.Click();

        return new CareersGeneralPage(Driver);
    }

    public InsightsPage OpenInsightsPage()
    {
        var topNavigationRow = Driver.WaitUntilInteractable(_topNavigationRowLocator);
        var insightsNavigationLink = topNavigationRow.FindElement(_insightsNavigationLinkLocator);
        insightsNavigationLink.Click();
        return new InsightsPage(Driver);
    }

    public SearchResultsPage SearchByKeyword(string keyword)
    {
        var searchButton = Driver.WaitUntilInteractable(_searchButtonLocator);
        searchButton.Click();

        var searchFieldTextbox = Driver.WaitUntilInteractable(_searchFieldTexboxLocator);
        searchFieldTextbox.Clear();
        searchFieldTextbox.SendKeys(keyword);

        var findButton = Driver.WaitUntilInteractable(_findButtonLocator);
        findButton.Click();
        
        return new SearchResultsPage(Driver);
    }
}
