using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;

namespace EpamWebsite.Tests.Steps;

[Binding]
public class FooterActionsSteps(TestContextHolder context)
{
    [When("I download '(.*)' from the footer")]
    public void WhenIDownloadFromTheFooter(string itemTitle)
    {
        context.LandingPage.AcceptCookies();
        context.LandingPage.WaitForCookiesBannerToDisappear();

        context.LandingPage.DownloadFooterItem(itemTitle);
    }

    [Then("File '(.*)' is downloaded successfully")]
    public void ThenFileIsDownloadedSuccessfully(string fileName)
    {
        var filePath = context.LandingPage.WaitForDownload(context.DriverManager.DownloadDirectory, fileName, TimeSpan.FromMinutes(1));

        Assert.That(Path.GetFileName(filePath), Is.EqualTo(fileName));
    }
}
