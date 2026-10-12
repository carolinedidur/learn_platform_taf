using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;

namespace EpamWebsite.Tests.Steps;

[Binding]
public class NavigationSteps(TestContextHolder context)
{
    private readonly LandingPage _landingPage = context.LandingPage;
    private ServicePage _servicePage;

    [When("I hover over the \"Services\" link in the main navigation menu")]
    public void WhenIHoverOverTheServicesLinkInTheMainNavigationMenu()
    {
        _landingPage.HoverOverServicesLink();
    }

    [When("I select service category '(.*)' from the dropdown")]
    public void WhenISelectServiceCategoryFromTheDropdown(string category)
    {
        _servicePage = _landingPage.SelectServiceCategory(category);
    }

    [Then("The page contains '(.*)' title")]
    public void ThenThePageContainsTitle(string title)
    {
        var actualTitle = _servicePage.GetTitle();
        Assert.That(actualTitle, Is.EqualTo(title));
    }

    [Then("The section ‘Our Related Expertise’ is displayed on the page")]
    public void ThenTheSectionOurRelatedExpertiseIsDisplayedOnThePage()
    {
        var isOurRelatedExpertiseSectionDisplayed = _servicePage.IsOurRelatedExpertiseSectionDisplayed();
        Assert.That(isOurRelatedExpertiseSectionDisplayed, Is.True);
    }
}
