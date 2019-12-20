namespace Acr.Assist.CommentMicroService.Service.DataValidation
{
    public class ExceptionMessages
    {
        /// <summary>
        /// The null or empty
        /// </summary>
        public const string NullOrEmpty = "  should not be empty";

        /// <summary>
        /// The empty template
        /// </summary>
        internal const string _EmptyTemplate = "Provide a value for the ";

        /// <summary>
        /// The topic identifier empty
        /// </summary>
        internal const string TopicIDEmpty = _EmptyTemplate + "Topic ID";

        /// <summary>
        /// The topic name empty
        /// </summary>
        internal const string TopicNameEmpty = _EmptyTemplate + "Topic Name";

        /// <summary>
        /// The module identifier empty
        /// </summary>
        internal const string ModuleIDEmpty = _EmptyTemplate + "Module ID";

        /// <summary>
        /// The module name empty
        /// </summary>
        internal const string ModuleNameEmpty = _EmptyTemplate + "Module Name";

        /// <summary>
        /// The module version empty
        /// </summary>
        internal const string ModuleVersionEmpty = _EmptyTemplate + "Module Version";

        /// <summary>
        /// The skip count empty
        /// </summary>
        internal const string SkipCountEmpty = _EmptyTemplate + "Skip Count";

        /// <summary>
        /// The take count empty
        /// </summary>
        internal const string TakeCountEmpty = _EmptyTemplate + "Take Count";

        /// <summary>
        /// The comment identifier empty
        /// </summary>
        internal const string CommentIDEmpty = _EmptyTemplate + "Comment ID";

        /// <summary>
        /// The comment text empty
        /// </summary>
        internal const string CommentTextEmpty = _EmptyTemplate + "Comment Text";

        /// <summary>
        /// The created user empty
        /// </summary>
        internal const string CreatedUserEmpty = _EmptyTemplate + "Created User";

        /// <summary>
        /// The created user identifier empty
        /// </summary>
        internal const string CreatedUserIDEmpty = _EmptyTemplate + "Created User ID";

        /// <summary>
        /// The implemented comment empty
        /// </summary>
        internal const string ImplementedCommentEmpty = _EmptyTemplate + "Implemented Comment";

        /// <summary>
        /// The implemented user identifier empty
        /// </summary>
        internal const string ImplementedUserIDEmpty = _EmptyTemplate + "Implemented User ID";

        /// <summary>
        /// The implemented user empty
        /// </summary>
        internal const string ImplementedUserEmpty = _EmptyTemplate + "Implemented User";

        /// <summary>
        /// The implemented module version empty
        /// </summary>
        internal const string ImplementedModuleVersionEmpty = _EmptyTemplate + "Implemented Module Version";

        /// <summary>
        /// The user identifier empty
        /// </summary>
        internal const string UserIDEmpty = _EmptyTemplate + "UserID";

        /// <summary>
        /// The user empty
        /// </summary>
        internal const string UserEmpty = _EmptyTemplate + "User";

        /// <summary>
        /// The user name empty
        /// </summary>
        internal const string UserNameEmpty = _EmptyTemplate + "User Name";

        /// <summary>
        /// The email identifier empty
        /// </summary>
        internal const string EmailIDEmpty = _EmptyTemplate + "Email ID";

        /// <summary>
        /// The email identifier invalid
        /// </summary>
        internal const string EmailIDInvalid = "Invalid Email ID";

        /// <summary>
        /// The comment not found
        /// </summary>
        internal const string CommentNotFound = "Comment not found with specified id {0}";

        /// <summary>
        /// The take count greater than zero
        /// </summary>
        internal const string TakeCountGreaterThanZero = "Take Count always should be greather than 0";
    }
}
