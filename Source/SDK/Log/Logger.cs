using System;
using System.Collections.Generic;
using System.Reflection;

namespace PayPal.Log
{
    internal class Logger
    {
        private static IDictionary<Type, Logger> loggerKeyValuePairs = new Dictionary<Type, Logger>();

        private List<BaseLogger> baseLoggerList;

        private Logger(Type givenType)
        {
            baseLoggerList = new List<BaseLogger>();

            if (LogConfiguration.LoggerListInConfiguration != null)
            {
                foreach (string loggerName in LogConfiguration.LoggerListInConfiguration)
                {
                    Type loggerType = Type.GetType(loggerName);

                    if (loggerType != null)
                    {
                        Type[] types = { typeof(Type) };
                        ConstructorInfo infoConstructor = loggerType.GetConstructor( types );

                        if (infoConstructor != null)
                        {
                            try
                            {
                                object instance = infoConstructor.Invoke(new object[] { givenType });

                                if (instance is BaseLogger)
                                {
                                    baseLoggerList.Add((BaseLogger)instance);
                                }
                            }
                            catch
                            {
                                throw new ConfigException("Invalid Configuration. Please check 'PayPalLog' value in  <appSettings> section of your configuration file.");
                            }
                        }
                    }
                }

                if (baseLoggerList.Count > 0)
                {
                    ConfigureLoggers();
                }
            }
        }

        private void ConfigureLoggers()
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                loggerBase.IsEnabled = true;
            }
        }

        public static Logger GetLogger(Type givenType)
        {
            if (givenType == null)
            {
                throw new ArgumentNullException("givenType", "Type cannot be null");
            }

            Logger log;
            lock (loggerKeyValuePairs)
            {
                if (!loggerKeyValuePairs.TryGetValue(givenType, out log))
                {
                    log = new Logger(givenType);
                    loggerKeyValuePairs[givenType] = log;
                }
            }
            return log;
        }

        /// <summary>
        /// Call loggers' Debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsDebugEnabled)
                {
                    loggerBase.Debug(message);
                }
            }
        }

        /// <summary>
        /// Call loggers' Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(string message, System.Exception exception)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsDebugEnabled)
                {
                    loggerBase.Debug(message, exception);
                }
            }
        }

        /// <summary>
        /// Call loggers' DebugFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void DebugFormat(string format, params object[] args)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsDebugEnabled)
                {
                    loggerBase.DebugFormat(format, args);
                }
            }
        }

        /// <summary>
        /// Call loggers' Error
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsErrorEnabled)
                {
                    loggerBase.Error(message);
                }
            }
        }

        /// <summary>
        /// Call loggers' Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(string message, System.Exception exception)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsErrorEnabled)
                {
                    loggerBase.Error(message, exception);
                }
            }
        }

        /// <summary>
        /// Call loggers' ErrorFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void ErrorFormat(string format, params object[] args)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsErrorEnabled)
                {
                    loggerBase.ErrorFormat(format, args);
                }
            }
        }

        /// <summary>
        /// Call loggers' Info
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsInfoEnabled)
                {
                    loggerBase.Info(message);
                }
            }
        }

        /// <summary>
        /// Call loggers' Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(string message, System.Exception exception)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsInfoEnabled)
                {
                    loggerBase.Info(message, exception);
                }
            }
        }

        /// <summary>
        /// Call loggers' InfoFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void InfoFormat(string format, params object[] args)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsInfoEnabled)
                {
                    loggerBase.InfoFormat(format, args);
                }
            }
        }

        /// <summary>
        /// Call loggers' Warn
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsWarnEnabled)
                {
                    loggerBase.Warn(message);
                }
            }
        }

        /// <summary>
        /// Call loggers' Warn
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(string message, System.Exception exception)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsWarnEnabled)
                {
                    loggerBase.Warn(message, exception);
                }
            }
        }

        /// <summary>
        /// Call loggers' WarnFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WarnFormat(string format, params object[] args)
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                if (loggerBase.IsEnabled && loggerBase.IsWarnEnabled)
                {
                    loggerBase.WarnFormat(format, args);
                }
            }
        }

        /// <summary>
        /// Flush the loggers
        /// </summary>
        public void Flush()
        {
            foreach (BaseLogger loggerBase in baseLoggerList)
            {
                loggerBase.Flush();
            }
        }
    }
}
