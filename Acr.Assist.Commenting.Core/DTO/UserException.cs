namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class UserException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserException"/> class.
        /// </summary>
        public UserException()
        {
        }

        /// <summary>
        /// Generates the internal server exception.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public static UserException GenerateInternalServerException(string msg)
        {
            return new UserException(500, "Internal server exception", msg);
        }

        /// <summary>
        /// Generates the aurgument exception.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public static UserException GenerateAurgumentException(string msg)
        {
            return new UserException(400, "Invalid Paramters", msg);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserException"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="headder">The headder.</param>
        public UserException(int status, string headder)
        {
            StatusCode = status;
            Headder = headder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserException"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="headder">The headder.</param>
        /// <param name="descryption">The descryption.</param>
        public UserException(int status, string headder, string descryption)
        {
            StatusCode = status;
            Headder = headder;
            Description = descryption;
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the headder.
        /// </summary>
        public string Headder { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
