using System;

namespace PayPal.Log
{
    /// <summary>
    /// Abstract base for the loggers
    /// </summary>
    public abstract class BaseLogger
    {
        /// <summary>
        /// Gets the given type associated with this logger.
        /// </summary>
        public Type GivenType { get; private set; }

        /// <summary>
        /// Get or sets whether this logger is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of this logger and associates it with the specified object type.
        /// </summary>
        /// <param name="typeGiven">The type to associate with this logger.</param>
        public BaseLogger(Type typeGiven)
        {
            this.GivenType = typeGiven;
            this.IsEnabled = true;
        }

        /// <summary>
        /// Gets whether or not debug logging is enabled.
        /// </summary>
        public virtual bool IsDebugEnabled { get { return true; } }

        /// <summary>
        /// Gets whether or not error logging is enabled.
        /// </summary>
        public virtual bool IsErrorEnabled { get { return true; } }

        /// <summary>
        /// Gets whether or not informational logging is enabled.
        /// </summary>
        public virtual bool IsInfoEnabled { get { return true; } }

        /// <summary>
        /// Gets whether or not logging for warnings is enabled.
        /// </summary>
        public virtual bool IsWarnEnabled { get { return true; } }

        /// <summary>
        /// Records a debug message to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public abstract void Debug(string message);

        /// <summary>
        /// Records a debug message, including any exception details, to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">Exception details to be logged.</param>
        public abstract void Debug(string message, System.Exception exception);

        /// <summary>
        /// Records a formatted debug message to the log.
        /// </summary>
        /// <param name="format">A composite format string to be logged.</param>
        /// <param name="args">An array that contains zero or more objects to format.</param>
        public abstract void DebugFormat(string format, params object[] args);

        /// <summary>
        /// Records an error message to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public abstract void Error(string message);

        /// <summary>
        /// Records an error message, including any exception details, to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">Exception details to be logged.</param>
        public abstract void Error(string message, System.Exception exception);

        /// <summary>
        /// Records a formatted error message to the log.
        /// </summary>
        /// <param name="format">A composite format string to be logged.</param>
        /// <param name="args">An array that contains zero or more objects to format.</param>
        public abstract void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// Records an informational message to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public abstract void Info(string message);

        /// <summary>
        /// Records an informational message, including any exception details, to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">Exception details to be logged.</param>
        public abstract void Info(string message, System.Exception exception);

        /// <summary>
        /// Records a formatted informational message to the log.
        /// </summary>
        /// <param name="format">A composite format string to be logged.</param>
        /// <param name="args">An array that contains zero or more objects to format.</param>
        public abstract void InfoFormat(string format, params object[] args);

        /// <summary>
        /// Records a warning message to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public abstract void Warn(string message);

        /// <summary>
        /// Records a warning message, including any exception details, to the log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="exception">Exception details to be logged.</param>
        public abstract void Warn(string message, System.Exception exception);

        /// <summary>
        /// Records a formatted warning message to the log.
        /// </summary>
        /// <param name="format">A composite format string to be logged.</param>
        /// <param name="args">An array that contains zero or more objects to format.</param>
        public abstract void WarnFormat(string format, params object[] args);

        /// <summary>
        /// Flushes any resources or streams used by this logger.
        /// </summary>
        public virtual void Flush() { /* Default behavior is to do nothing. */ }
    }
}
