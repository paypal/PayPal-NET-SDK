// NuGet Install
// install PayPalCoreSDK -excludeversion -outputDirectory .\Packages
// log4net 2.0
using log4net;
using PayPal.PayPal.Api.Errors;

namespace PayPal.Exception
{
    public class PayPalException : System.Exception
    {
		private Error errorDetail = null;

        /// <summary>
        /// Logs output statements, errors, debug info to a text file    
        /// </summary>
        private static readonly ILog logger = LogManagerWrapper.GetLogger(typeof(PayPalException));

        /// <summary>
		/// Represents application configuration errors 
		/// </summary>
        public PayPalException() : base() { }
        
		/// <summary>
		/// Represents errors that occur during application execution
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public PayPalException(string message): base(message)
		{
			if (logger.IsErrorEnabled)
			{
				logger.Error(message, this);
			}
		}

		/// <summary>
		/// Represents errors that occur during application execution
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="cause">The exception that is the cause of the current exception</param>
        public PayPalException(string message, System.Exception cause) : base(message, cause)
		{
			if (logger.IsErrorEnabled) 
			{
				logger.Error(message, this);
			}
		}

				/// <summary>
		/// Represents errors that occur during application execution
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="error">Error details returned from the server</param>
		public PayPalException(string message, Error error)	: this(message)
		{
			errorDetail = error;
		}

		/// <summary>
		/// Represents errors that occur during application execution
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="cause">The exception that is the cause of the current exception</param>
		/// <param name="error">Error details returned from the server</param>
		public PayPalException(string message, System.Exception cause, Error error)	: this(message, cause)
		{
			errorDetail = error;
		}

		/// <summary>
		/// Gets any error details if applicable
		/// </summary>
		public Error ErrorDetail
		{
			get
			{
				return errorDetail;
			}
		}
    }
}
