using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;

namespace EpamWebsite.Tests.Steps;

[Binding]
public class GlobalSearchSteps(TestContextHolder context)
{
    private readonly LandingPage _landingPage = context.LandingPage;
    private SearchResultsPage _searchResultsPage;

    [When("I perform global search for '(.*)'")]
    public void WhenIPerformGlobalSearchFor(string keyword)
    {
        _searchResultsPage = _landingPage.SearchByKeyword(keyword);
    }

    [Then("All the search results contain '(.*)'")]
    public void ThenAllTheSearchResultsContain(string keyword)
    {
        var searchResultLinks = _searchResultsPage.GetAllSearchResults();

        var mismatches = searchResultLinks
            .Select(link => link.Text)
            .Where(text => !text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.That(mismatches, Is.Empty, $"Links without '{keyword}': {string.Join(" | ", mismatches)}");
    }
}
