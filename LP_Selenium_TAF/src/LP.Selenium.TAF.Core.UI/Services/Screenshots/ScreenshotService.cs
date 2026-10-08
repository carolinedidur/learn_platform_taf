using LP.Selenium.TAF.Core.UI.Driver.DriverManager;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Services.Screenshots;

public static class ScreenshotService
{
    public static string TakeScreenshot(IDriverManager driverManager, string testName)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "Screenshots");
        Directory.CreateDirectory(directory);

        var invalid = Path.GetInvalidFileNameChars();
        var safeName = string.Join("_", testName.Split(invalid, StringSplitOptions.RemoveEmptyEntries));

        var path = Path.Combine(directory, $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        ((ITakesScreenshot)driverManager.Driver).GetScreenshot().SaveAsFile(path);
        return path;
    }
}
