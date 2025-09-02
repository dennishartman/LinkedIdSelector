using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace LinkedIdSelector.Model
{
    public class NLogger
    {
        public event EventHandler<string> LogReceived;
        public static void Setup()
        {
            // Check if NLog configuration exists, if not, create a new one
            if (LogManager.Configuration == null)
            {
                // Create a new NLog configuration
                var config = new LoggingConfiguration();

                DateTime dateTime = DateTime.Now;

                // Define file target
                var fileTarget = new FileTarget
                {
                    FileName = $"C:\\logFiles\\log_{dateTime}.txt",
                    Layout = "${longdate} ${level} ${message} ${exception}"
                };

                // Define rules for logging
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

                // Apply the configuration
                LogManager.Configuration = config;
            }
        }

        public void LogInfo(string message) => LogReceived?.Invoke(this, message);
    }
}
