using Microsoft.AspNetCore.Authorization;

namespace Acr.Assist.CommentMicroService
{
    /// <summary>
    /// Represensts the requirement for user Id 
    /// </summary>
    public class UserIdRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the user identifier claim.
        /// </summary>
        public string UserIdClaim { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdRequirement"/> class.
        /// </summary>
        /// <param name="userIdClaim">The user identifier claim.</param>
        public UserIdRequirement(string userIdClaim)
        {
            UserIdClaim = userIdClaim;
        }
    }
}
