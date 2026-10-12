using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;

namespace EpamWebsite.Tests.Steps;

[Binding]
public class InsightsPageSteps(TestContextHolder context)
{
    private InsightsPage _insightsPage;
    private InsightArticlePage _insightArticlePage;
    private string _carouselTitle;

    [When("I open the Insights page")]
    public void WhenIOpenTheInsightsPage()
    {
        context.LandingPage.AcceptCookies();
        context.LandingPage.WaitForCookiesBannerToDisappear();

        _insightsPage = context.LandingPage.OpenInsightsPage();
    }

    [When("I open the article from the carousel after (.*) swipes")]
    public void WhenIOpenTheArticleFromTheCarouselAfterSwipes(int numberOfSwipes)
    {
        _insightsPage.SwipeCarousel(numberOfSwipes);
        _carouselTitle = _insightsPage.GetActiveSlideTitle();

        _insightArticlePage = _insightsPage.OpenActiveSlideArticle();
    }

    [Then("The article title matches the carousel slide title")]
    public void ThenTheArticleTitleMatchesTheCarouselSlideTitle()
    {
        var articleTitle = _insightArticlePage.GetArticleTitle();
        Assert.That(articleTitle, Is.EqualTo(_carouselTitle));
    }
}
