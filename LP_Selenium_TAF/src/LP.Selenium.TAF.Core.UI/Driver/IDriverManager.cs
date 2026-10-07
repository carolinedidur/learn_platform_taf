using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Driver;

public interface IDriverManager
{
    public IWebDriver Driver { get; }

    public string DownloadDirectory { get; }

    public void StartBrowser();

    public void QuitBrowser();
}
