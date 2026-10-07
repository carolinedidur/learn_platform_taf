using Microsoft.Extensions.Configuration;
using NUnit.Framework.Internal;
using PageObjects;
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
    private LandingPage _landingPage;

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

        _landingPage = new LandingPage(_webDriverWrapper);
    }

    [TestCase(".NET","Georgia")]
    [TestCase("Python", "Portugal")]
    [TestCase("Java", "Ukraine")]
    public void SearchPosition_ByCriteria_LatestResultContainsKeyword(string language, string country)
    {

        var careersGeneralPage = _landingPage.OpenCareersGeneralPage();

        var careersSearchPage = careersGeneralPage.OpenCareersSearchPage();

        careersSearchPage.AcceptCookies();
        careersSearchPage.WaitForCookiesBannerToDisappear();

        careersSearchPage.SearchRemotePosition(language, country);

        careersSearchPage.WaitForLoaderCycle();

        var jobDetailsPage = careersSearchPage.OpenFirstFoundJob();

        var jobTitle = jobDetailsPage.GetJobTitle();

        Assert.That(jobTitle, Does.Contain(language));
    }

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void GlobalSearch_ByKeyword_AllResultLinksContainKeyword(string keyword)
    {
        var searchResultsPage = _landingPage.SearchByKeyword(keyword);

        var searchResultLinks = searchResultsPage.GetAllSearchResults();

        var mismatches = searchResultLinks
            .Select(link => link.Text)
            .Where(text => !text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.That(mismatches, Is.Empty, $"Links without '{keyword}': {string.Join(" | ", mismatches)}");
    }

    [TestCase("Code of Ethical Conduct", "Code_of_Ethical_Conduct.pdf")]
    public void DownloadFile_FromFooter_FileNameMatchesExpected(string itemTitle, string expectedFileName)
    {
        _landingPage.AcceptCookies();
        _landingPage.WaitForCookiesBannerToDisappear();

        _landingPage.ScrollFooterIntoView();

        _landingPage.DownloadFooterItem(itemTitle);

        var filePath = _landingPage.WaitForDownload(_driverManager.DownloadDirectory, expectedFileName, TimeSpan.FromMinutes(1));

        Assert.That(Path.GetFileName(filePath), Is.EqualTo(expectedFileName));
    }

    [TestCase(2)]
    [TestCase(3)]
    public void InsightsCarousel_AfterSwipe_ArticleTitleMatchesCarouselTitle(int numberOfSwipes)
    {
        _landingPage.AcceptCookies();
        _landingPage.WaitForCookiesBannerToDisappear();

        var insightsPage = _landingPage.OpenInsightsPage();

        insightsPage.SwipeCarousel(numberOfSwipes);
        var carouselTitle = insightsPage.GetActiveSlideTitle();

        var insightsArticlePage = insightsPage.OpenActiveSlideArticle();
        var articleTitle = insightsArticlePage.GetArticleTitle();

        Assert.That(carouselTitle, Is.EqualTo(articleTitle));
    }

    [TearDown]
    public void TearDown()
    {
        _driverManager.QuitBrowser();
    }
}
