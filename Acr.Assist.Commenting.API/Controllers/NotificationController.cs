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
    /// Commnent notification operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1/Comment/{commentID}/Notification")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class NotificationController : BaseController
    {
        /// <summary>
        /// The comment notification service
        /// </summary>
        private ICommentNotificationService commentNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        /// <param name="commentNotificationService">The comment notification service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public NotificationController(
            ICommentNotificationService commentNotificationService,
            ILogger<NotificationController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.commentNotificationService = commentNotificationService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Adds the user Comment Notification
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="notificationEntry">Contains the commentNotificationEntry</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCommentNotification(Guid commentID, CommentNotificationEntry notificationEntry)
        {
            try
            {
                await commentNotificationService.AddCommentNotification(commentID, notificationEntry);
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
        /// Deletes the user comment notification
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="userID">Contains the unique id of user</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentNotification(Guid commentID, string userID)
        {
            try
            {
                await commentNotificationService.DeleteCommentNotification(commentID, userID);
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