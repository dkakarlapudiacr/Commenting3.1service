using System;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class CommentNotification
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the notification status.
        /// </summary>
        public NotificationStatus NotificationStatus { get; set; }

        /// <summary>
        /// Gets or sets the notified date time.
        /// </summary>
        public DateTime NotifiedDateTime { get; set; }
    }
}
