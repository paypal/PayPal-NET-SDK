using PayPal.Log;

namespace PayPal
{
    /// <summary>
    /// Represents an error that occurred in the PayPal SDK.
    /// </summary>
    public class PayPalException : System.Exception
    {
        /// <summary>
        /// Logs output statements, errors, debug info to a text file
        /// </summary>
        private static Logger logger = Logger.GetLogger(typeof(PayPalException));

        /// <summary>
        /// Initializes a new <seealso cref="PayPalException"/> with no exception details set.
        /// </summary>
        public PayPalException() : this(string.Empty) { }

        /// <summary>
        /// Represents errors that occur during application execution
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public PayPalException(string message): this(message, null) { }

        /// <summary>
        /// Initializes a new <seealso cref="PayPalException"/> and sets the exception message and cause.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="cause">The exception that is the cause of the current exception</param>
        public PayPalException(string message, System.Exception cause) : base(message, cause)
        {
            this.LogDefaultMessage(message);
        }

        /// <summary>
        /// Copy constructor provided by convenience for derived classes.
        /// </summary>
        /// <param name="ex">The original exception to copy information from.</param>
        protected PayPalException(PayPalException ex) : base(ex.Message, ex.InnerException) { }

        /// <summary>
        /// Optional prefix that will be prepended to the error written to the log.
        /// This allows for exceptions to be distinguishable in the log by their type while
        /// preserving the exception heirarchy.
        /// </summary>
        protected virtual string ExceptionMessagePrefix { get { return string.Empty; } }

        /// <summary>
        /// Helper method that logs a message related to this exception to the logfile.
        /// </summary>
        /// <param name="message">The exception message to be logged.</param>
        protected void LogMessage(string message)
        {
            this.LogMessage(message, null);
        }

        /// <summary>
        /// Helper method that logs a message related to this exception to the logfile.
        /// </summary>
        /// <param name="message">The exception message to be logged.</param>
        /// <param name="ex">Optional System.Exception object to include in the log message.</param>
        protected void LogMessage(string message, System.Exception ex)
        {
            string errorMessage = string.IsNullOrEmpty(this.ExceptionMessagePrefix) ? message : string.Format("{0}: {1}", this.ExceptionMessagePrefix, message);
            if (ex == null)
            {
                logger.Error(errorMessage);
            }
            else
            {
                logger.Error(errorMessage, ex);
            }
        }

        /// <summary>
        /// Helper method for logging a message when this object is created.
        /// Derived classes can override this to log more specific exception
        /// information without cluttering up the logfile.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        protected virtual void LogDefaultMessage(string message)
        {
            this.LogMessage(message, this);
        }
    }
}
