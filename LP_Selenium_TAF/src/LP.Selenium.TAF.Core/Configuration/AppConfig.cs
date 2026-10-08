namespace LP.Selenium.TAF.Core.Configuration;

public class AppConfig
{
    public EnvironmentConfig Environment { get; set; } = new ();

    public DriverConfig Driver { get; set; } = new ();

    public LoggerConfig Logger { get; set; } = new ();
}
