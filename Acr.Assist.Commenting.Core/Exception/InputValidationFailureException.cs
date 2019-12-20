using System;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.Exception
{
    /// <summary>
    /// Exception thrown  input validation failures
    /// </summary>
    public class InputValidationFailureException : ApplicationException
    {
        /// <summary>
        /// Gets or sets the list of errors
        /// </summary>
        public Dictionary<string, string> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationFailureException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public InputValidationFailureException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationFailureException"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public InputValidationFailureException(Dictionary<string, string> errors) : base()
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationFailureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public InputValidationFailureException(string message, Dictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationFailureException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
        public InputValidationFailureException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
