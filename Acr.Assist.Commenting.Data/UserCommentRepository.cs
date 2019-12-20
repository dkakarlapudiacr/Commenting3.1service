using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using Acr.Assist.CommentMicroService.Core.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using Acr.Assist.CommentMicroService.Core.DTO;
using System.Linq;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the user comment related repository operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.IUserCommentRepository" />
    public class UserCommentRepository : IUserCommentRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="mongoDBName">Name of the mongo database.</param>
        public UserCommentRepository(string connectionString, string mongoDBName)
        {
            dbContext = new MongoContext.MongoContext(connectionString, mongoDBName);
        }

        /// <summary>
        /// Gets the un viwed comment.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<UserUnViewedComment>> GetUnViwedComment(string userID)
        {
            var userIDReegex = new Regex(userID, RegexOptions.IgnoreCase);

            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);

            var NotCreatedByQuery = !Builders<Comment>.Filter.Regex(i => i.CreatedUserID, new BsonRegularExpression("/^" + userID + "$/i"));

            var EmptyViwesQuery = Builders<Comment>.Filter.Eq(i => i.CommentViews, null);

            var NotinCommentviewsQuery = Builders<Comment>.Filter.ElemMatch(c => c.CommentViews, v => userIDReegex.IsMatch(v.UserID));

            var CommentViwesQuery = Builders<Comment>.Filter.Or(EmptyViwesQuery, !NotinCommentviewsQuery);

            var query = Builders<Comment>.Filter.And(

             NotCreatedByQuery,
             CommentViwesQuery
            );

            var res = await commentColletion.Find(query)
                .Project(X =>
                new UserUnViewedComment
                {
                    CommentID = X.CommentID,
                    ModuleID = X.ModuleID,
                    ModuleName = X.ModuleName,
                    TopicName = X.TopicName,
                    CommentText = X.CommentText,
                    CreatedUser = X.CreatedUser,
                    CreatedUserID = X.CreatedUserID,
                    CreatedUserImagePath = X.CreatedUserImagePath,
                    UpdatedDateTime = X.UpdatedDateTime
                }).ToListAsync();

            return res.OrderByDescending(i => i.UpdatedDateTime).ToList();
        }

        /// <summary>
        /// Updates the comments as viewed.
        /// </summary>
        /// <param name="userCommentsView">The user comments view.</param>
        /// <param name="commentView">The comment view.</param>
        public async Task UpdateCommentsAsViewed(UserCommentsViewEntry userCommentsView, CommentView commentView)
        {
            var userIDReegex = new Regex(userCommentsView.UserID, RegexOptions.IgnoreCase);
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);
            var NotCreatedByUserQuery = !Builders<Comment>.Filter.Regex(i => i.CreatedUserID, new BsonRegularExpression("/^" + userCommentsView.UserID + "$/i"));
            var NotinViewsQuery = !Builders<Comment>.Filter.ElemMatch(c => c.CommentViews, v => userIDReegex.IsMatch(v.UserID));

            var query = Builders<Comment>.Filter.And(
                  Builders<Comment>.Filter.Eq(i => i.ModuleID, userCommentsView.ModuleID),
                  Builders<Comment>.Filter.Eq(i => i.TopicName, userCommentsView.TopicID),
                  NotCreatedByUserQuery,
                NotinViewsQuery
                  );

            var update = Builders<Comment>.Update
                    .Push(i => i.CommentViews, commentView);

            await commentColletion.UpdateManyAsync(query, update);
        }
    }
}
