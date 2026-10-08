using LP.Selenium.TAF.Core.UI.Services.Download;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Driver.DriverManager;

public interface IDriverManager
{
    public IWebDriver Driver { get; }

    public IDownloadService DownloadService { get; }

    public void StartBrowser();

    public void QuitBrowser();
}
