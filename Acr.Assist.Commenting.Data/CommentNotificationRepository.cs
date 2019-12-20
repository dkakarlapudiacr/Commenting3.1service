using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the comment notification related repository operations
    /// </summary>
    public class CommentNotificationRepository : ICommentNotificationRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotificationRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public CommentNotificationRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Adds the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentNotification">The comment notification.</param>
        public async Task AddCommentNotification(Guid commentID, CommentNotification commentNotification)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var update = Builders<Comment>.Update
                    .Push(i => i.CommentNotificationUsers, commentNotification);


            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Deletes the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        public async Task DeleteCommentNotification(Guid commentID, string userID)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var delQuery = Builders<CommentNotification>.Filter.Eq(i => i.UserID, userID);
            var update = Builders<Comment>.Update
                     .PullFilter(i => i.CommentNotificationUsers, delQuery);

            await commentColletion.UpdateOneAsync(query, update);
        }
    }
}
