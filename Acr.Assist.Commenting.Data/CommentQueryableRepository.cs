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
    /// Class for the comment querable related repository operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.ICommentQueryableRepository" />
    public class CommentQueryableRepository : ICommentQueryableRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentQueryableRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public CommentQueryableRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<Comment>> GetComments(CommentsFilter filter)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);
            var query = Builders<Comment>.Filter.And(
                                            Builders<Comment>.Filter.Eq(i => i.TopicName, filter.TopicID),
                                            Builders<Comment>.Filter.Eq(i => i.ModuleID, filter.ModuleID)
                                            );
           return await commentColletion.Find(query).Skip(filter.SkipCount).Limit(filter.TakeCount).ToListAsync(); 
        }
    }
}
