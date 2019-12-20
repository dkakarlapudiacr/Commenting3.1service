using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Acr.Assist.CommentMicroService.Core.Integrations.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Acr.Assist.CommentMicroService
{
    ///<summary>
    /// Filter that checks if the User specified by the UserName exists in Assist
    /// </summary>
    public class UserIdExistsRequirementHandler : AuthorizationHandler<UserIdRequirement>, IAuthorizationRequirement
    {
        /// <summary>
        /// The authorization micro service
        /// </summary>
        private readonly IAuthorizationMicroService authorizationMicroService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserIdExistsRequirementHandler> logger;

        /// <summary>
        /// The authorization configuration
        /// </summary>
        private readonly AuthorizationConfig authorizationConfig;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdExistsRequirementHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="authorizationMicroService">The authorization micro service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="authorizationConfig">The authorization configuration.</param>
        public UserIdExistsRequirementHandler(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationMicroService authorizationMicroService, 
            ILogger<UserIdExistsRequirementHandler> logger,
            AuthorizationConfig authorizationConfig)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authorizationMicroService = authorizationMicroService;
            this.logger = logger;
            this.authorizationConfig = authorizationConfig;
        }

        /// <summary>
        /// Check if  requirement has been handled
        /// </summary>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIdRequirement requirement)
        {
            try
            {
                var claimsIdentity = context.User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var userIdClaim = claimsIdentity.FindFirst(c => c.Type == requirement.UserIdClaim &&
                                     c.Issuer == authorizationConfig.Issuer);
                    if (userIdClaim != null)
                    {
                        HttpContext httpContext = httpContextAccessor.HttpContext;
                        var accessToken = httpContext.Request.Headers["Authorization"];
                        var input = new AccountRejectionStatusRequest
                        {
                            AccessToken = accessToken,
                            UserId = userIdClaim.Value
                        };
                        var isUserRejected = authorizationMicroService.CheckIfUserIsRejected(input).Result;
                        if (!isUserRejected)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "UserIdExistsRequirement::HandleRequirementAsync");
                return Task.CompletedTask;
            }
        }
    }
}
