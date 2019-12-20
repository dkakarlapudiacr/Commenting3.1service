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
    /// comments operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1")]
    [Consumes("application/json")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class CommentsController : BaseController
    {
        /// <summary>
        /// The comment service
        /// </summary>
        private ICommentService commentService;

        /// <summary>
        /// The authorization micro service
        /// </summary>
        private readonly IAuthorizationMicroService authorizationMicroService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        /// <param name="authorizationMicroService">The authorization micro service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public CommentsController(
            ICommentService commentService,
            IAuthorizationMicroService authorizationMicroService,
            ILogger<NotificationController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.commentService = commentService;
            this.authorizationMicroService = authorizationMicroService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Returns the list of comments for module topic 
        /// </summary>
        /// <param name="filter">Contains the commentsFilter</param>
        /// <returns></returns>
        [Route("Comments")]
        [HttpGet]
        public async Task<IActionResult> GetComments(CommentsFilter filter)
        {
            var comments = await commentService.GetComments(filter);
            return Ok(comments);
        }

        /// <summary>
        /// Adds a comment for module topic
        /// </summary>
        /// <param name="commentEntry">Contains the addCommentEntry</param>
        /// <returns></returns>
        [Route("Comment")]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody]AddCommentEntry commentEntry)
        {
            var res = await commentService.AddComment(commentEntry); 
            return Ok(res);
        }

        /// <summary>
        /// Deletes the comment
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <returns></returns>
        [Route("Comment/{commentID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid commentID)
        {
            var commentIDDetails = new CommentIDDetails() { CommentID = commentID };
            await commentService.DeleteComment(commentIDDetails);
            return Ok();
        }

        /// <summary>
        /// Edits the comment
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="commentEntry">Contains the updateCommentEntry</param>
        /// <returns></returns>
        [Route("Comment/{commentID}")]
        [HttpPut]
        public async Task<IActionResult> EditComment(Guid commentID, [FromBody]UpdateCommentEntry commentEntry)
        {
            var res = await commentService.EditComment(commentID, commentEntry);
            return Ok(res);
        }

        /// <summary>
        /// Propose a Comment
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="userEntry">Contains the commentUserEntry</param>
        /// <returns></returns>
        [Route("Comment/{commentID}/Propose")]
        [HttpPatch]
        public async Task<IActionResult> ProposeComment(Guid commentID, [FromBody]CommentUserEntry userEntry)
        {
            await commentService.ProposeComment(commentID, userEntry);
            return Ok();
        }

        /// <summary>
        /// Marks the comment as implemented
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="commentImplementEntry">Contains the commentImplementEntry</param>
        /// <returns></returns>
        [Route("Comment/{commentID}/Implement")]
        [HttpPatch]
        public async Task<IActionResult> ImplementComment(Guid commentID, CommentImplementEntry commentImplementEntry)
        {
            var res = await commentService.ImplementComment(commentID, commentImplementEntry);
            return Ok(res);
        }

        /// <summary>
        /// Deletes the comment based on module id and topic id
        /// </summary>    /// <param name="moduleId"></param>
        /// <param name="topicName"></param>
        /// <returns></returns>
        [Route("Comment/{moduleId}/{topicName}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopicComments(string moduleId, string topicName)
        {
            await commentService.DeleteTopicComments(new DeleteComment() {  ModuleID= moduleId, TopicName = topicName });
            return Ok();
        }
    }
}