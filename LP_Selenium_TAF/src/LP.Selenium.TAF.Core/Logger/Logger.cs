using log4net;
using log4net.Config;

namespace LP.Selenium.TAF.Core.Logger;

public sealed class Logger : ILogger
{
    private static readonly ILog _log = CreateLog();

    public void Debug(string message)
    {
        _log.Debug(message);
    }

    public void Info(string message)
    {
        _log.Info(message);
    }

    public void Warning(string message)
    {
        _log.Warn(message);
    }

    public void Error(string message)
    {
        _log.Error(message);
    }

    public void Error(string message, Exception exception)
    {
        _log.Error(message, exception);
    }

    private static ILog CreateLog()
    {
        GlobalContext.Properties["BaseDir"] = AppContext.BaseDirectory;

        var configFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "Config", "log4net.config"));
        XmlConfigurator.Configure(LogManager.GetRepository(typeof(Logger).Assembly), configFile);

        return LogManager.GetLogger(typeof(Logger));
    }
}
