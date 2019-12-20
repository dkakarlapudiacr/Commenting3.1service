using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    /// Class for the module comment related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.IModuleCommentService" />
    public class ModuleCommentService : IModuleCommentService
    {
        /// <summary>
        /// The module comment repository
        /// </summary>
        private readonly IModuleCommentRepository moduleCommentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCommentService"/> class.
        /// </summary>
        /// <param name="moduleCommentRepository">The module comment repository.</param>
        public ModuleCommentService(
            IModuleCommentRepository moduleCommentRepository)
        {
            this.moduleCommentRepository = moduleCommentRepository;
        }

        /// <summary>
        /// Gets the comment details.
        /// </summary>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<List<ModuleCommentDetails>> GetCommentDetails(string moduleID)
        {
            if (string.IsNullOrEmpty(moduleID) || string.IsNullOrWhiteSpace(moduleID))
            {
                throw new InputValidationFailureException(ExceptionMessages.ModuleIDEmpty);
            }
            return await this.moduleCommentRepository.GetCommentDetails(moduleID);
        }
    }
}
