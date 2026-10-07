using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverManager.Driver;

public interface IDriverManager
{
    public IWebDriver Driver { get; }
    public string DownloadDirectory { get; }
    public void StartBrowser();
    public void QuitBrowser();
}
