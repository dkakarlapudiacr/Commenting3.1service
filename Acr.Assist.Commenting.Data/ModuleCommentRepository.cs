using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the module comment related repository operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.IModuleCommentRepository" />
    public class ModuleCommentRepository : IModuleCommentRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCommentRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public ModuleCommentRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Gets the comment details.
        /// </summary>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        public async Task<List<ModuleCommentDetails>> GetCommentDetails(string moduleID)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            var query = Builders<CommentEntry>.Filter.Eq(i => i.ModuleID, moduleID);


            return await commentColletion.Find(query).Project(X => new ModuleCommentDetails { CommentID = X.CommentID, TopicName = X.TopicName }).ToListAsync();
        }
    }
}
