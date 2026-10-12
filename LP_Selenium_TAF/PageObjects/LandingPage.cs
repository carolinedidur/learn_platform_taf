using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverWrapper;

namespace PageObjects;

public class LandingPage(IWebDriverWrapper driver) : BasePage(driver)
{
    private static readonly By _topNavigationRowLocator = By.ClassName("top-navigation__row");
    private static readonly By _careersNavigationLinkLocator = By.LinkText("Careers");
    private static readonly By _insightsNavigationLinkLocator = By.LinkText("Insights");
    private static readonly By _servicesNavigationLinkLocator = By.LinkText("Services");
    private static readonly By _searchFieldTexboxLocator = By.Name("q");
    private static readonly By _searchButtonLocator = By.CssSelector("button[class*='header-search__button']");
    private static readonly By _findButtonLocator = By.XPath("//button[descendant::span[@class='bth-text-layer']]");
    private static readonly string _categoryLinkLocator = "//li[@class='top-navigation__sub-item' and descendant::a[normalize-space()='{0}']]";

    private IWebElement TopNavigationRow => Driver.WaitUntilInteractable(_topNavigationRowLocator);

    public CareersGeneralPage OpenCareersGeneralPage()
    {
        var careersNavigationLink = TopNavigationRow.FindElement(_careersNavigationLinkLocator);
        careersNavigationLink.Click();

        return new CareersGeneralPage(Driver);
    }

    public InsightsPage OpenInsightsPage()
    {
        var insightsNavigationLink = TopNavigationRow.FindElement(_insightsNavigationLinkLocator);
        insightsNavigationLink.Click();
        return new InsightsPage(Driver);
    }

    public void HoverOverServicesLink()
    {
        var servicesNavigationLink = TopNavigationRow.FindElement(_servicesNavigationLinkLocator);
        Driver.HoverOverElement(servicesNavigationLink);
    }

    public ServicePage SelectServiceCategory(string category)
    {
        var servicesNavigationLink = TopNavigationRow.FindElement(_servicesNavigationLinkLocator);
        var specificServiceNavigationLink = TopNavigationRow.FindElement(By.XPath(string.Format(_categoryLinkLocator, category)));
        Driver.HoverAndClick(servicesNavigationLink, specificServiceNavigationLink);
        return new ServicePage(Driver);
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
