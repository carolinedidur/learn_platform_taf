using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class SearchResultsPage(IWebDriverWrapper driver)
    : BasePage(driver)
{
    private static readonly By _searchResultItemLocator = By.XPath("//div[@class='search-results__items']/child::article");

    public IReadOnlyCollection<IWebElement> GetAllSearchResults()
    {
        var searchResultLinks = Driver.Wait.Until(d =>
        {
            var foundLinks = d.FindElements(_searchResultItemLocator);
            return foundLinks.Count > 0 ? foundLinks : null;
        });

        return searchResultLinks;
    }
}
