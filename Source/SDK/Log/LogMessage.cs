using System;
using System.Globalization;

namespace PayPal.Log
{
    /// <summary>
    /// Log message
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Gets the arguments to be used in logging a formatted string.
        /// </summary>
        public object[] Args { get; private set; }

        /// <summary>
        /// Gets the <seealso cref="IFormatProvider"/> that provides the formatted string.
        /// </summary>
        public IFormatProvider Provider { get; private set; }

        /// <summary>
        /// Gets the string format to be used when logging.
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Constructor that logs the specified message.
        /// </summary>
        /// <param name="message"></param>
        public LogMessage(string message) : this(CultureInfo.InvariantCulture, message) { }

        /// <summary>
        /// Constructor that logs the specified formatted message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public LogMessage(string format, params object[] args) : this(CultureInfo.InvariantCulture, format, args) { }

        /// <summary>
        /// Constructor that logs the specified formatted message using the defined provider.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public LogMessage(IFormatProvider provider, string format, params object[] args)
        {
            this.Args = args;
            this.Format = format;
            this.Provider = provider;
        }

        /// <summary>
        /// Converts the stored message details into a string to be logged.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string formatted = string.Empty;

            if (Args.Length > 0)
            {
                formatted = string.Format(Provider, Format, Args);
            }
            else
            {
                formatted = Format;
            }

            return formatted;
        }
    }
}
