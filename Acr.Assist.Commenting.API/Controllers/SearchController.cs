using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;

namespace Acr.Assist.CommentMicroService.Controllers
{
    /// <summary>
    /// Comment search operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/Comments/Search")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class SearchController : BaseController
    {
        /// <summary>
        /// The comment queryable service
        /// </summary>
        private ICommentQueryableService commentQueryableService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="commentQueryableService">The comment queryable service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public SearchController(
            ICommentQueryableService commentQueryableService, 
            ILogger<SearchController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.commentQueryableService = commentQueryableService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Returns the list of Comments with all details
        /// </summary>
        /// <param name="filter">Contains the CommentsFilter</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetComments(CommentsFilter filter)
        {
            try
            {
                var res = await commentQueryableService.GetComments(filter);
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
    }
}