using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Acr.Assist.CommentMicroService.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acr.Assist.CommentMicroService.Controllers
{
    /// <summary>
    /// User comments operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1/user/")]
    [Consumes("application/json")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class UserCommentController : BaseController
    {
        /// <summary>
        /// The user comment service/
        /// </summary>
        private IUserCommentService userCommentService;

        /// <summary>
        /// The authorization micro service
        /// </summary>
        private readonly IAuthorizationMicroService authorizationMicroService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentController"/> class.
        /// </summary>
        /// <param name="userCommentService">The user comment service.</param>
        /// <param name="authorizationMicroService">The authorization micro service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public UserCommentController(
            IUserCommentService userCommentService,
            IAuthorizationMicroService authorizationMicroService,
            ILogger<NotificationController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.userCommentService = userCommentService;
            this.authorizationMicroService = authorizationMicroService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Gets the un viwed comments.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        [Route("{userID}/UnViwedComments")]
        [HttpGet]
        public async Task<IActionResult> GetUnViwedComments(string userID)
        {
            try
            {
                var res = await userCommentService.GetUnViwedComment(userID);
                return Ok(res);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, UserException.GenerateAurgumentException(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, UserException.GenerateAurgumentException(e.Message));
            }
        }

        /// <summary>
        /// Updates the Comments as Viewed
        /// </summary>
        /// <param name="userCommentsView">Contains the userCommentsView</param>
        /// <returns></returns>
        [Route("UpdateCommentsAsViewed")]
        [HttpPut]
        public async Task<IActionResult> UpdateCommentsAsViewed(UserCommentsViewEntry userCommentsView)
        {
            try
            {
                await userCommentService.UpdateCommentsAsViewed(userCommentsView);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, UserException.GenerateAurgumentException(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, UserException.GenerateAurgumentException(e.Message));
            }
        }
    }
}