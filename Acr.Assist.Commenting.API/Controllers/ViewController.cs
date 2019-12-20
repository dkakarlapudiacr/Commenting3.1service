using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Controllers
{
    /// <summary>
    /// View comments operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1/Comment/{commentID}/View")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class ViewController : BaseController
    {
        /// <summary>
        /// The comment view service
        /// </summary>
        private ICommentViewService commentViewService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewController"/> class.
        /// </summary>
        /// <param name="commentViewService">The comment view service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public ViewController(
            ICommentViewService commentViewService,
            ILogger<NotificationController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.commentViewService = commentViewService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Marks the Comment as Viewed
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="userEntry">Contains the user entry details</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCommentView(Guid commentID, [FromBody]CommentUserEntry userEntry)
        {
            try
            {
                await commentViewService.AddCommentView(commentID, userEntry);
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

        /// <summary>
        /// UnMarks the Comment as Viewed
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="userID">Contains the unique id of user</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentView(Guid commentID, [FromBody]string userID)
        {
            try
            {
                await commentViewService.DeleteCommentView(commentID, userID);
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