using EpamWebsite.Tests.Support;
using PageObjects;
using TechTalk.SpecFlow;

namespace EpamWebsite.Tests.Steps;

[Binding]
public class CommonSteps(TestContextHolder context)
{
    [Given(@"I navigate to the EPAM website")]
    public void GivenINavigateToTheEpamWebsite()
    {
        context.LandingPage = new LandingPage(context.Driver);
    }
}