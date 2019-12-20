using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the comment suggestions related repository operations 
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.ICommentSuggestionRepository" />
    public class CommentSuggestionRepository : ICommentSuggestionRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSuggestionRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public CommentSuggestionRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Adds the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentSuggestion">The comment suggestion.</param>
        public async Task AddCommentSuggestion(Guid commentID, CommentSuggestion commentSuggestion)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var update = Builders<Comment>.Update.Inc(i => i.TotalSuggestedCount, 1).AddToSet(i => i.CommentSuggestions, commentSuggestion);

            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Deletes the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        public async Task DeleteCommentSuggestion(Guid commentID, string userID)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var delQuery = Builders<CommentSuggestion>.Filter.Eq(i => i.UserID, userID);
            var update = Builders<Comment>.Update
                     .PullFilter(i => i.CommentSuggestions, delQuery)
                      .Inc(i => i.TotalSuggestedCount, -1); ;

            await commentColletion.UpdateOneAsync(query, update);
        }
    }
}
