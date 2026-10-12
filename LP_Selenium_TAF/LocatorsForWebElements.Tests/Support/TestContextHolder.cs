using PageObjects;
using WebDriverManager.Driver;
using WebDriverManager.DriverWrapper;

namespace EpamWebsite.Tests.Support;

public class TestContextHolder
{
    public IDriverManager DriverManager { get; set; } = null!;
    public IWebDriverWrapper Driver { get; set; } = null!;
    public LandingPage LandingPage { get; set; } = null!;
}
