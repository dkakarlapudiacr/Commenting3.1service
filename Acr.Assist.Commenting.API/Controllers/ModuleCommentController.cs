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
    /// Module comment operations handled by this controller
    /// </summary>
    /// <seealso cref="BaseController" />
    [Produces("application/json")]
    [Route("commenting/api/v1/Module/{moduleID}")]
    [Authorize(Policy = "UserIdExists")]
    [ApiController]
    public class ModuleCommentController : BaseController
    {
        /// <summary>
        /// The module comment service
        /// </summary>
        private IModuleCommentService moduleCommentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCommentController"/> class.
        /// </summary>
        /// <param name="moduleCommentService">The module comment service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public ModuleCommentController(
            IModuleCommentService moduleCommentService,
            ILogger<ModuleCommentController> logger,
            IConfigurationManager configuration) :
            base(configuration)
        {
            this.moduleCommentService = moduleCommentService;
            LoggerInstance = logger;
        }

        /// <summary>
        /// Returns the list of Comments for a given module
        /// </summary>
        /// <param name="moduleID">Contains the unique Id of the module</param>
        /// <returns></returns>
        [Route("Comments")]
        [HttpGet]
        public async Task<IActionResult> GetComments(string moduleID)
        {
            try
            {
                var res = await moduleCommentService.GetCommentDetails(moduleID);
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