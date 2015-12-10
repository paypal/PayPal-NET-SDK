namespace PayPal
{
    /// <summary>
    /// Represents an error that occurred in the PayPal SDK when a hash or security algorithm is not supported.
    /// </summary>
    public class AlgorithmNotSupportedException : PayPalException
    {
		/// <summary>
		/// Represents errors where a hash or security algorithm is not supported.
		/// </summary>
		/// <param name="message">The message that describes the error</param>
        public AlgorithmNotSupportedException(string message) : base(message) { }

        /// <summary>
        /// Gets the prefix to use when logging the exception information.
        /// </summary>
        protected override string ExceptionMessagePrefix { get { return "Algorithm Not Supported"; } }
	}
}
