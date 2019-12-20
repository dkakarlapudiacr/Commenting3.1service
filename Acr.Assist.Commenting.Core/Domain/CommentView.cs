using System;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class CommentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentView"/> class.
        /// </summary>
        public CommentView()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentView"/> class.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="user">The user.</param>
        public CommentView(string userID, string user)
        {
            UserID = userID;
            User = user;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the viewed date time.
        /// </summary>
        public DateTime ViewedDateTime { get; set; }
    }
}
