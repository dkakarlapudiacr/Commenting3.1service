using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acr.Assist.CommentMicroService.Controllers
{
    /// <summary>
    /// Comments endorse operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1/Comment/{commentID}/Endorse")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class EndorseController : BaseController
    {
        /// <summary>
        /// The comment suggestion service
        /// </summary>
        private ICommentSuggestionService commentSuggestionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndorseController"/> class.
        /// </summary>
        /// <param name="commentSuggestionService">The comment suggestion service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public EndorseController(
            ICommentSuggestionService commentSuggestionService,
            ILogger<EndorseController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.commentSuggestionService = commentSuggestionService;
            LoggerInstance = logger;
        }

        /// <summary>
        ///  Marks the comment as suggested
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="user">Contains the commentUserEntry</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCommentSuggestion(Guid commentID, [FromBody]CommentUserEntry user)
        {
            try
            {
                var res = await commentSuggestionService.AddCommentSuggestion(commentID, user);
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
        /// Deletes the user comment suggestion
        /// </summary>
        /// <param name="commentID">Contains the unique id of comment</param>
        /// <param name="userID">Contains the unique id of user</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentSuggestion(Guid commentID, string userID)
        {
            try
            {
                await commentSuggestionService.DeleteCommentSuggestion(commentID, userID);
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