using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverManager.DriverWrapper;

public interface IWebDriverWrapper
{
    public WebDriverWait Wait { get; }
    public IWebElement WaitUntilVisible(By locator);
    public IWebElement WaitUntilInteractable(By locator);
    public T WaitUntilWithCustomTimeout<T>(Func<IWebDriver, T?> condition, int timeout);
    public void NavigateTo(string url);
}
