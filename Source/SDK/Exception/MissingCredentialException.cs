namespace PayPal
{
    /// <summary>
    /// Represents an error that occurred in the PayPal SDK when application credentials are required.
    /// </summary>
    public class MissingCredentialException : PayPalException
    {
		/// <summary>
		/// Represents errors where certain credential information is required but missing.
		/// </summary>
		/// <param name="message">The message that describes the error</param>
        public MissingCredentialException(string message) : base(message) { }

        /// <summary>
        /// Gets the prefix to use when logging the exception information.
        /// </summary>
        protected override string ExceptionMessagePrefix { get { return "Missing Credential"; } }
	}
}
