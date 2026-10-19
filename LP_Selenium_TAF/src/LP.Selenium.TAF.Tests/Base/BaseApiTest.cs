using LP.Selenium.TAF.Core.API.Client;
using LP.Selenium.TAF.Core.API.Factories;
using NUnit.Framework.Interfaces;

namespace LP.Selenium.TAF.Tests.Base;

public class BaseApiTest : BaseTestFixture
{
    protected IApiClient ApiClient { get; private set; }

    [SetUp]
    public void SetUp()
    {
        Logger.Info($"Test started: {TestContext.CurrentContext.Test.Name}");

        ApiClient = ApiClientFactory.Create(ConfigurationService);
    }

    [TearDown]
    public void TearDown()
    {
        var result = TestContext.CurrentContext.Result;

        if (result.Outcome.Status == TestStatus.Failed)
        {
            Logger.Error($"Test failed: {result.Message}");
        }
        else
        {
            Logger.Info($"Test finished: {result.Outcome.Status}");
        }
    }
}
