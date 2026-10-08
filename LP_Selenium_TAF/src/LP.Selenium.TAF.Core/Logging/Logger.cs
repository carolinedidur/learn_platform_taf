using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using LP.Selenium.TAF.Core.Configuration;

namespace LP.Selenium.TAF.Core.Logging;

public sealed class Logger(IConfigurationService configurationService) : ILogger
{
    private readonly ILog _log = CreateLog(configurationService);

    public static ILogger Instance { get => field ?? throw new InvalidOperationException("Call Logger.Initialize() first."); private set; }

    public static void Initialize(IConfigurationService configurationService)
    {
        Instance = new Logger(configurationService);
    }

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

    private static ILog CreateLog(IConfigurationService configurationService)
    {
        GlobalContext.Properties["BaseDir"] = AppContext.BaseDirectory;

        var configFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "Logging", "log4net.config"));
        XmlConfigurator.Configure(LogManager.GetRepository(typeof(Logger).Assembly), configFile);

        var repository = (Hierarchy)LogManager.GetRepository(typeof(Logger).Assembly);
        XmlConfigurator.Configure(repository, configFile);

        repository.Root.Level = repository.LevelMap[configurationService.Configuration.Logger.MinLevel]
                                ?? Level.Info;

        return LogManager.GetLogger(typeof(Logger));
    }
}
