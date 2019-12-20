using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the comment view related repository operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.ICommentViewRepository" />
    public class CommentViewRepository : ICommentViewRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public CommentViewRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Adds the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentView">The comment view.</param>
        public async Task AddCommentView(Guid commentID, CommentView commentView)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var update = Builders<Comment>.Update
                    .Push(i => i.CommentViews, commentView);


            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Deletes the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        public async Task DeleteCommentView(Guid commentID, string userID)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
             var delQuery = Builders<CommentView>.Filter.Eq(i => i.UserID, userID);
            var update = Builders<Comment>.Update
                     .PullFilter(i => i.CommentViews, delQuery);
           
            await commentColletion.UpdateOneAsync(query,update);
        }
    }
}
