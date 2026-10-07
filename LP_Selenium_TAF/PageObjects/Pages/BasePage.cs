using WebDriverManager.DriverWrapper;

namespace PageObjects.Pages;

public class BasePage
{
    protected BasePage(IWebDriverWrapper driver)
    {
        ArgumentNullException.ThrowIfNull(driver);
        Driver = driver;
    }

    protected IWebDriverWrapper Driver { get;  }
}
