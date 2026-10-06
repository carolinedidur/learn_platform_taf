using Microsoft.Extensions.Configuration;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.Driver;
using WebDriverManager.DriverWrapper;

namespace LocatorsForWebelements.Tests;

[TestFixture]
public class Tests
{
    private IDriverManager _driverManager = null!;
    private IWebDriverWrapper _webDriverWrapper = null!;
    private string _baseUrl = string.Empty;
    private int _timeout;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["Environment:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl missing.");
        _timeout = config.GetValue<int?>("Driver:Timeout") ?? throw new InvalidOperationException("Timeout missing.");
    }


    [SetUp]
    public void Setup()
    {
        _driverManager = new DriverManager();
        _webDriverWrapper = new WebDriverWrapper(_driverManager, TimeSpan.FromSeconds(_timeout));

        _driverManager.StartBrowser();
        _webDriverWrapper.NavigateTo(_baseUrl);
    }

    [TestCase(".NET","Georgia")]
    [TestCase("Python", "Portugal")]
    [TestCase("Java", "Ukraine")]
    public void SearchPosition_ByCriteria_LatestResultContainsKeyword(string language, string country)
    {
        var topNavigationRow = _webDriverWrapper.WaitUntilInteractable(By.ClassName("top-navigation__row"));
        topNavigationRow.FindElement(By.LinkText("Careers")).Click();

        var startSearchButton = _webDriverWrapper.WaitUntilInteractable(By.ClassName("pinned-button"));
        startSearchButton.FindElement(By.PartialLinkText("START YOUR SEARCH")).Click();

        _webDriverWrapper.WaitUntilInteractable(By.XPath("//button[normalize-space()='Accept All']")).Click();

        _webDriverWrapper.Wait.Until(d => !d.FindElements(By.CssSelector("div[aria-label='Cookie banner']")).Any(e => e.Displayed));

        var locationSelectDropdown = _webDriverWrapper.WaitUntilInteractable(By.Id("react-select-2-input"));
        locationSelectDropdown.Clear();
        locationSelectDropdown.SendKeys(country);

        _driverManager.Driver.FindElement(By.XPath($"//div[contains(@id,'-option-') and normalize-space()='{country}']")).Click();

        var searchKeywordTextbox = _webDriverWrapper.WaitUntilInteractable(By.XPath("//input[@data-testid='search-input']"));
        searchKeywordTextbox.Clear();
        searchKeywordTextbox.SendKeys(language);

        WaitForLoaderCycle();

        _webDriverWrapper.WaitUntilInteractable(By.XPath("//div[contains(@class,'sideMenu')]//label[contains(@for,'checkbox-vacancy_type-Remote')]")).Click();

        _webDriverWrapper.WaitUntilInteractable(By.XPath("//form/button[@data-testid='buttonComponent']")).Click();

        WaitForLoaderCycle();

        var firstJobCard = _webDriverWrapper.Wait.Until(d =>
        {
            var cards = d.FindElements(By.CssSelector("div[class^='JobCard_panel_']"));
            return cards.Count > 0 ? cards[0] : null;
        })!;

        firstJobCard.FindElement(By.TagName("a")).Click();

        var jobTitle = _webDriverWrapper.WaitUntilVisible(By.CssSelector("h1[data-testid='job-details-banner-title']")).Text;

        Assert.That(jobTitle, Does.Contain(language));
    }

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void GlobalSearch_ByKeyword_AllResultLinksContainKeyword(string keyword)
    {
        _driverManager.Driver.FindElement(By.CssSelector("button[class*='header-search__button']")).Click();

        var searchFieldTextbox = _webDriverWrapper.WaitUntilInteractable(By.Name("q"));
        searchFieldTextbox.Clear();
        searchFieldTextbox.SendKeys(keyword);

        _driverManager.Driver.FindElement(By.XPath("//button[descendant::span[@class='bth-text-layer']]")).Click();

        var searchResultLinks = _webDriverWrapper.Wait.Until(d =>
        {
            var foundLinks = d.FindElements(By.XPath("//div[@class='search-results__items']/child::article"));
            return foundLinks.Count > 0 ? foundLinks : null;
        });

        var mismatches = searchResultLinks
            .Select(link => link.Text)
            .Where(text => !text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.That(mismatches, Is.Empty, $"Links without '{keyword}': {string.Join(" | ", mismatches)}");
    }

    [TearDown]
    public void TearDown()
    {
        _driverManager.QuitBrowser();
    }

    private void WaitForLoaderCycle()
    {
        try
        {
            new WebDriverWait(_driverManager.Driver, TimeSpan.FromSeconds(3))
                .Until(d => d.FindElements(By.CssSelector("div[data-testid='preloader']")).Any(e => e.Displayed));
        }
        catch (WebDriverTimeoutException)
        {
            return;
        }

        _webDriverWrapper.Wait.Until(d => !d.FindElements(By.CssSelector("div[data-testid='preloader']")).Any(e => e.Displayed));
    }
}
