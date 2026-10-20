using System.Text.RegularExpressions;
using System.Xml.Linq;
using LP.Selenium.TAF.Core.UI.Driver.DriverManager;
using OpenQA.Selenium;

namespace LP.Selenium.TAF.Core.UI.Services.Screenshots;

public static class ScreenshotService
{
    public static string TakeScreenshot(IDriverManager driverManager, string testName)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "Screenshots");
        Directory.CreateDirectory(directory);

        var safeName = Regex.Replace(testName, @"[""<>:|*?\\/\r\n]", "_");

        var path = Path.Combine(directory, $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        ((ITakesScreenshot)driverManager.Driver).GetScreenshot().SaveAsFile(path);
        return path;
    }
}
