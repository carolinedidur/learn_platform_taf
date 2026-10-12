using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;
namespace EpamWebsite.Tests.Steps;

[Binding]
public class CareersSearchSteps(TestContextHolder context)
{
    private LandingPage _landingPage;
    private CareersSearchPage _careersSearchPage;
    private JobDetailsPage _jobDetailsPage;

    [Given(@"I navigate to the EPAM careers search page")]
    public void GivenINavigateToTheEPAMCareersSearchPage()
    {
        _landingPage = new LandingPage(context.Driver);
        _careersSearchPage = _landingPage.OpenCareersGeneralPage().OpenCareersSearchPage();
    }

    [When(@"I search for remote positions with '(.*)' in '(.*)'")]
    public void WhenISearchForRemotePositions(string language, string country)
    {
        _careersSearchPage.AcceptCookies();
        _careersSearchPage.WaitForCookiesBannerToDisappear();

        _careersSearchPage.SearchRemotePosition(language, country);
    }

    [When(@"I open the first found position")]
    public void WhenIOpenTheFirstFoundPosition()
    {
        _careersSearchPage.WaitForLoaderCycle();
        _jobDetailsPage = _careersSearchPage.OpenFirstFoundJob();
    }

    [Then(@"The position page title contains '(.*)'")]
    public void ThenThePositionPageTitleContains(string language)
    {
        var jobTitle = _jobDetailsPage.GetJobTitle();
        Assert.That(jobTitle, Does.Contain(language));
    }
}
