using System;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class CommentSuggestion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSuggestion"/> class.
        /// </summary>
        public CommentSuggestion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSuggestion"/> class.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="user">The user.</param>
        public CommentSuggestion(string userID, string user)
        {
            UserID = userID;
            User = user;
        }
        /// <summary>
        /// Gets or sets the suggested date time.
        /// </summary>
        public DateTime SuggestedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string User { get; set; }

    }
}
