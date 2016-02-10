using System;
using System.Configuration;
using System.Collections.Generic;

namespace PayPal.Log
{
    /// <summary>
    /// Configuration for PayPalCoreSDK
    /// </summary>
    public static class LogConfiguration
    {
        /// <summary>
        /// Key for the loggers to be set in <appSettings><add key="PayPalLogger" value="PayPal.Log.DiagnosticsLogger, PayPal.Log.Log4netLogger"/></appSettings> in configuration file
        /// </summary>
        public const string PayPalLogKey = "PayPalLogger";

        /// <summary>
        /// AppSettings configuration key that defines the delimiter to be used when specifying the list of loggers.
        /// </summary>
        public const string PayPalLogDelimiterKey = "PayPalLogger.Delimiter";

        /// <summary>
        /// Default delimiter to use for the <see cref="PayPalLogDelimiterKey"/> value.
        /// </summary>
        public const string PayPalLogDefaultDelimiter = ",";

        private static List<string> configurationLoggerList = GetConfigurationLoggerList();

        /// <summary>
        /// Gets the list of loggers from the config.
        /// </summary>
        public static List<string> LoggerListInConfiguration
        {
            get
            {
                return configurationLoggerList;
            }
        }

        private static List<string> GetConfigurationLoggerList()
        {
            List<string> loggerList = new List<string>();

            var value = GetConfiguration(PayPalLogKey);
            var delimiter = GetConfiguration(PayPalLogDelimiterKey);

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if(string.IsNullOrEmpty(delimiter))
            {
                delimiter = PayPalLogDefaultDelimiter; // Default is a comma-separated list.
            }

            var splitList = new List<string>(value.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries));

            if (splitList == null || splitList.Count == 0)
            {
                return null;
            }

            foreach(string split in splitList)
            {
                if (!loggerList.Contains(split.Trim()))
                {
                    loggerList.Add(split.Trim());
                }
            }

            return loggerList;
        }

        private static string GetConfiguration(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}
