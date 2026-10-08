using LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Pages;

public class CareersSearchPage(IWebDriverWrapper driver)
    : BasePage(driver)
{
    private static readonly By _loaderLocator = By.CssSelector("div[data-testid='preloader']");
    private static readonly By _locationSelectDropdownLocator = By.Id("react-select-2-input");
    private static readonly By _searchKeywordTextboxLocator = By.XPath("//input[@data-testid='search-input']");
    private static readonly By _remoteCheckboxLocator = By.XPath("//div[contains(@class,'sideMenu')]//label[contains(@for,'checkbox-vacancy_type-Remote')]");
    private static readonly By _searchButtonLocator = By.XPath("//form/button[@data-testid='buttonComponent']");
    private static readonly By _jobCardLocator = By.CssSelector("div[class^='JobCard_panel_']");
    private static readonly By _jobCardTitleLinkLocator = By.TagName("a");
    private static readonly string _countryOptionLocator = "//div[contains(@id,'-option-') and normalize-space()='{0}']";

    public void SearchRemotePosition(string language, string country)
    {
        var locationSelectDropdown = Driver.WaitUntilInteractable(_locationSelectDropdownLocator);
        locationSelectDropdown.Clear();
        locationSelectDropdown.SendKeys(country);

        var countryOption = Driver.WaitUntilInteractable(By.XPath(string.Format(_countryOptionLocator, country)));
        countryOption.Click();

        WaitForLoaderCycle();

        var searchKeywordTextbox = Driver.WaitUntilInteractable(_searchKeywordTextboxLocator);
        searchKeywordTextbox.Clear();
        searchKeywordTextbox.SendKeys(language);

        WaitForLoaderCycle();

        var searchButton = Driver.WaitUntilInteractable(_searchButtonLocator);
        searchButton.Click();

        WaitForLoaderCycle();

        var remoteCheckBox = Driver.WaitUntilInteractable(_remoteCheckboxLocator);
        remoteCheckBox.Click();
    }

    public JobDetailsPage OpenFirstFoundJob()
    {
        var firstJobCard = Driver.Wait.Until(d =>
        {
            var cards = d.FindElements(_jobCardLocator);
            return cards.Count > 0 ? cards[0] : null;
        })!;

        var firstJobTitleLink = firstJobCard.FindElement(_jobCardTitleLinkLocator);
        firstJobTitleLink.Click();

        return new JobDetailsPage(Driver);
    }

    public void WaitForLoaderCycle()
    {
        try
        {
            Driver.WaitUntilWithCustomTimeout(d => d.FindElements(_loaderLocator).Any(e => e.Displayed), TimeSpan.FromSeconds(1));
        }
        catch (WebDriverTimeoutException)
        {
            return;
        }

        Driver.Wait.Until(d => !d.FindElements(_loaderLocator).Any(e => e.Displayed));
    }
}
