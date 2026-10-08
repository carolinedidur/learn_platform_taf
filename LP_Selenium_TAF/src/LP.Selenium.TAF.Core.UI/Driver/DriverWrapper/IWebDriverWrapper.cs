using LP.Selenium.TAF.Core.UI.Services.Download;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LP.Selenium.TAF.Core.UI.Driver.DriverWrapper;

public interface IWebDriverWrapper
{
    public WebDriverWait Wait { get; }

    public IDownloadService DownloadService { get; }

    public IWebElement WaitUntilVisible(By locator);

    public IWebElement WaitUntilInteractable(By locator);

    public string WaitForDownload(string expectedFileName, TimeSpan timeout);

    public T WaitUntilWithCustomTimeout<T>(Func<IWebDriver, T?> condition, TimeSpan timeout, string? message = null);

    public void NavigateTo(string url);

    public void ScrollElementIntoView(IWebElement element);
}
