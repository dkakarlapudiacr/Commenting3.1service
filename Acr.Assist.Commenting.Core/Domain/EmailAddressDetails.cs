namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class EmailAddressDetails
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>
        public string LastName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressDetails"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="displayName">The display name.</param>
        public EmailAddressDetails(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;
        }
    }
}
