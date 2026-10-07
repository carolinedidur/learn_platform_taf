using OpenQA.Selenium;
using WebDriverManager.DriverWrapper;

namespace PageObjects.Pages;

public class SearchResultsPage(IWebDriverWrapper driver) : BasePage(driver)
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
