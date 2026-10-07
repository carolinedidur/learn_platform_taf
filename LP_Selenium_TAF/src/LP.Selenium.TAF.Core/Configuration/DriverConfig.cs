namespace LP.Selenium.TAF.Core.Configuration;

public class DriverConfig
{
    public string Browser { get; set; } = string.Empty;

    public bool Headless { get; set; }

    public int Timeout { get; set; }
}
