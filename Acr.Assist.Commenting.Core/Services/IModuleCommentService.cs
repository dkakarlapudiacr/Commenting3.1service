using Acr.Assist.CommentMicroService.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the module comment related operations for a user
    /// </summary>
    public interface IModuleCommentService
    {
        /// <summary>
        /// Gets the comment details.
        /// </summary>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        Task<List<ModuleCommentDetails>> GetCommentDetails(string moduleID);
    }
}
