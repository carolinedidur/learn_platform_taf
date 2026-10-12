using EpamWebsite.Tests.Support;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using WebDriverManager.Driver;
using WebDriverManager.DriverWrapper;

namespace EpamWebsite.Tests.Hooks;

[Binding]
public class Hooks(TestContextHolder context)
{
    private static string _baseUrl = string.Empty;
    private static int _timeout;

    [BeforeFeature]
    public static void BeforeFeature()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["Environment:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl missing.");
        _timeout = config.GetValue<int?>("Driver:Timeout") ?? throw new InvalidOperationException("Timeout missing.");
    }


    [BeforeScenario]
    public void BeforeScenario()
    {
        context.DriverManager = new DriverManager();
        context.Driver = new WebDriverWrapper(context.DriverManager, TimeSpan.FromSeconds(_timeout));

        context.DriverManager.StartBrowser();
        context.Driver.NavigateTo(_baseUrl);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        context.DriverManager.QuitBrowser();
    }
}
