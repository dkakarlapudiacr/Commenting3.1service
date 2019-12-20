using System;

namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class CommentUpdateResult
    {
        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        public Guid CommentID { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        public DateTime UpdatedDateTime { get; set; }

    }
}
