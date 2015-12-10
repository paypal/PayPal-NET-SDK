using System;
using System.Reflection;

namespace PayPal.Log
{
    /// <summary>
    /// Wrapper for reflected log4net
    /// </summary>
    internal class Log4netLogger : BaseLogger
    {
        private enum Status { NotInitialized, Failure, Loading, Success };

        private static Status currentStatus = Status.NotInitialized;
        private static readonly object syncLock = new object();

        private static Type paypalLogLogger;

        private static Type log4netCoreLoggerManager;
        private static MethodInfo log4netCoreLoggerManagerGetMethodGetLogger;
        private object log4netCoreLoggerManagerGetMethodGetLoggerInvoke;

        private static Type log4netCoreILogger;

        private static Type log4netCoreLevel;
        private static object log4netCoreLevelDebug;
        private static object log4netCoreLevelError;
        private static object log4netCoreLevelInfo;
        private static object log4netCoreLevelWarn;

        private static MethodInfo log4netCoreILoggerGetMethodLog;
        private static MethodInfo log4netCoreILoggerGetMethodIsEnabledFor;

        private static Type log4netUtilSystemStringFormat;

        private bool? isDebugEnabled;
        private bool? isErrorEnabled;
        private bool? isInfoEnabled;
        private bool? isWarnEnabled;

        /// <summary>
        /// Interrogate log4net
        /// </summary>
        private static void Reflect()
        {
            lock (Log4netLogger.syncLock)
            {
                if (currentStatus != Status.NotInitialized)
                {
                    return;
                }

                currentStatus = Status.Loading;
                try
                {
                    paypalLogLogger = Type.GetType("PayPal.Log.Logger");
                    log4netCoreLoggerManager = Type.GetType("log4net.Core.LoggerManager, log4net");

                    if (log4netCoreLoggerManager == null)
                    {
                        currentStatus = Status.Failure;
                        return;
                    }

                    log4netCoreLoggerManagerGetMethodGetLogger = log4netCoreLoggerManager.GetMethod("GetLogger", new Type[] { typeof(Assembly), typeof(Type) });

                    log4netCoreILogger = Type.GetType("log4net.Core.ILogger, log4net");
                    log4netCoreLevel = Type.GetType("log4net.Core.Level, log4net");

                    log4netCoreLevelDebug = log4netCoreLevel.GetField("Debug").GetValue(null);
                    log4netCoreLevelError = log4netCoreLevel.GetField("Error").GetValue(null);
                    log4netCoreLevelInfo = log4netCoreLevel.GetField("Info").GetValue(null);
                    log4netCoreLevelWarn = log4netCoreLevel.GetField("Warn").GetValue(null);

                    log4netUtilSystemStringFormat = Type.GetType("log4net.Util.SystemStringFormat, log4net");

                    log4netCoreILoggerGetMethodLog = log4netCoreILogger.GetMethod("Log", new Type[] { typeof(Type), log4netCoreLevel, typeof(object), typeof(System.Exception) });
                    log4netCoreILoggerGetMethodIsEnabledFor = log4netCoreILogger.GetMethod("IsEnabledFor", new Type[] { log4netCoreLevel });

                    if (log4netCoreLoggerManagerGetMethodGetLogger == null ||
                        log4netCoreILoggerGetMethodIsEnabledFor == null ||
                        log4netCoreILogger == null ||
                        log4netCoreLevel == null ||
                        log4netCoreILoggerGetMethodLog == null)
                    {
                        currentStatus = Status.Failure;
                        return;
                    }

                    if (LogConfiguration.LoggerListInConfiguration.Contains("PayPal.Log.Log4netLogger"))
                    {
                        Type log4netXmlConfigurator = Type.GetType("log4net.Config.XmlConfigurator, log4net");
                        if (log4netXmlConfigurator != null)
                        {
                            MethodInfo log4netXmlConfiguratorMethod = log4netXmlConfigurator.GetMethod("Configure", Type.EmptyTypes);
                            if (log4netXmlConfiguratorMethod != null)
                            {
                                log4netXmlConfiguratorMethod.Invoke(null, null);
                            }
                        }
                    }
                    currentStatus = Status.Success;
                }
                catch
                {
                    currentStatus = Status.Failure;
                }
            }
        }

        public Log4netLogger(Type givenType) : base(givenType)
        {
            if (currentStatus == Status.NotInitialized)
            {
                Reflect();
            }

            if (log4netCoreLoggerManager == null)
            {
                return;
            }

            this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke = log4netCoreLoggerManagerGetMethodGetLogger.Invoke(null, new object[] { Assembly.GetCallingAssembly(), givenType });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog IsDebugEnabled
        /// </summary>
        public override bool IsDebugEnabled
        {
            get
            {
                if (!isDebugEnabled.HasValue)
                {
                    if (currentStatus != Status.Success ||
                        this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke == null ||
                        paypalLogLogger == null ||
                        log4netUtilSystemStringFormat == null ||
                        log4netCoreLevelDebug == null)
                    {
                        isDebugEnabled = false;
                    }
                    else
                    {
                        isDebugEnabled = Convert.ToBoolean(log4netCoreILoggerGetMethodIsEnabledFor.Invoke(this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke, new object[] { log4netCoreLevelDebug }));
                    }
                }
                return isDebugEnabled.Value;
            }
        }

        /// <summary>
        /// Override the wrapper for log4net ILog IsErrorEnabled
        /// </summary>
        public override bool IsErrorEnabled
        {
            get
            {
                if (!isErrorEnabled.HasValue)
                {
                    if (currentStatus != Status.Success ||
                        this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke == null ||
                        paypalLogLogger == null ||
                        log4netUtilSystemStringFormat == null ||
                        log4netCoreLevelError == null)
                    {
                        isErrorEnabled = false;
                    }
                    else
                    {
                        isErrorEnabled = Convert.ToBoolean(log4netCoreILoggerGetMethodIsEnabledFor.Invoke(this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke, new object[] { log4netCoreLevelError }));
                    }
                }
                return isErrorEnabled.Value;
            }
        }

        /// <summary>
        /// Override the wrapper for log4net ILog IsInfoEnabled
        /// </summary>
        public override bool IsInfoEnabled
        {
            get
            {
                if (!isInfoEnabled.HasValue)
                {
                    if (currentStatus != Status.Success ||
                        this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke == null ||
                        paypalLogLogger == null ||
                        log4netUtilSystemStringFormat == null ||
                        log4netCoreLevelInfo == null)
                    {
                        isInfoEnabled = false;
                    }
                    else
                    {
                        isInfoEnabled = Convert.ToBoolean(log4netCoreILoggerGetMethodIsEnabledFor.Invoke(this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke, new object[] { log4netCoreLevelInfo }));
                    }
                }
                return isInfoEnabled.Value;
            }
        }


        /// <summary>
        /// Override the wrapper for log4net ILog IsInfoEnabled
        /// </summary>
        public override bool IsWarnEnabled
        {
            get
            {
                if (!isWarnEnabled.HasValue)
                {
                    if (currentStatus != Status.Success ||
                        this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke == null ||
                        paypalLogLogger == null ||
                        log4netUtilSystemStringFormat == null ||
                        log4netCoreLevelWarn == null)
                    {
                        isWarnEnabled = false;
                    }
                    else
                    {
                        isWarnEnabled = Convert.ToBoolean(log4netCoreILoggerGetMethodIsEnabledFor.Invoke(this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke, new object[] { log4netCoreLevelWarn }));
                    }
                }
                return isWarnEnabled.Value;
            }
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Debug
        /// </summary>
        /// <param name="message"></param>
        public override void Debug(string message)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelDebug,
                    new LogMessage(message),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public override void Debug(string message, System.Exception exception)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelDebug,
                    new LogMessage(message),
                    exception
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog DebugFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void DebugFormat(string format, params object[] args)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelDebug,
                    new LogMessage(format, args),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Error
        /// </summary>
        /// <param name="message"></param>
        public override void Error(string message)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelError,
                    new LogMessage(message),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public override void Error(string message, System.Exception exception)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelError,
                    new LogMessage(message),
                    exception
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog ErrorFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void ErrorFormat(string format, params object[] args)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelError,
                    new LogMessage(format, args),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Info
        /// </summary>
        /// <param name="message"></param>
        public override void Info(string message)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelInfo,
                    new LogMessage(message),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public override void Info(string message, System.Exception exception)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelInfo,
                    new LogMessage(message),
                    exception
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog InfoFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void InfoFormat(string format, params object[] args)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelInfo,
                    new LogMessage(format, args),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Warn
        /// </summary>
        /// <param name="message"></param>
        public override void Warn(string message)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelWarn,
                    new LogMessage(message),
                    null
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog Warn
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public override void Warn(string message, System.Exception exception)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelWarn,
                    new LogMessage(message),
                    exception
                });
        }

        /// <summary>
        /// Override the wrapper for log4net ILog WarnFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void WarnFormat(string format, params object[] args)
        {
            log4netCoreILoggerGetMethodLog.Invoke(
                this.log4netCoreLoggerManagerGetMethodGetLoggerInvoke,
                new object[]
                {
                    paypalLogLogger,
                    log4netCoreLevelWarn,
                    new LogMessage(format, args),
                    null
                });
        }
    }
}
