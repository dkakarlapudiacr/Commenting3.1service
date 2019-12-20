using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Data.MongoContext;
using MongoDB.Driver;

namespace Acr.Assist.CommentMicroService.Data
{
    /// <summary>
    /// Class for the comment related repository operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Data.ICommentRepository" />
    public class CommentRepository : ICommentRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private MongoContext.MongoContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public CommentRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="commentEntry">The comment entry.</param>
        /// <returns></returns>
        public async Task<Guid> AddComment(CommentEntry commentEntry)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            await commentColletion.InsertOneAsync(commentEntry);
            return commentEntry.CommentID;
        }

        /// <summary>
        /// Checks if comment exist.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <returns></returns>
        public async Task<bool> CheckIfCommentExist(Guid commentID)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            var comment = await commentColletion.FindAsync(i => i.CommentID == commentID);
            if (comment != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        public async Task DeleteComment(Guid commentID)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            await commentColletion.DeleteOneAsync(i => i.CommentID == commentID);
        }

        /// <summary>
        /// Edits the comment.
        /// </summary>
        /// <param name="commentUpdate">The comment update.</param>
        public async Task EditComment(CommentUpdateResult commentUpdate)
        {

            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            var query = Builders<CommentEntry>.Filter.Eq(i => i.CommentID, commentUpdate.CommentID);
            var update = Builders<CommentEntry>.Update
                                         .Set(x => x.CommentText, commentUpdate.CommentText)
                                         .Set(x => x.UpdatedDateTime, commentUpdate.UpdatedDateTime);

            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<CommentEntry>> GetComments(CommentsFilter filter)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            var query = Builders<CommentEntry>.Filter.And(
                                            Builders<CommentEntry>.Filter.Eq(i => i.TopicName, filter.TopicID),
                                            Builders<CommentEntry>.Filter.Eq(i => i.ModuleID, filter.ModuleID)
                                            );
            return await commentColletion.Find(query).SortByDescending(s => s.UpdatedDateTime).Skip(filter.SkipCount).Limit(filter.TakeCount).ToListAsync();
        }

        /// <summary>
        /// Implements the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentImplementEntry">The comment implement entry.</param>
        /// <param name="dateTime">The date time.</param>
        public async Task ImplementComment(Guid commentID, CommentImplementEntry commentImplementEntry, DateTime dateTime)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);
            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var update = Builders<Comment>.Update
                                         .Set(x => x.ImplementedComment, commentImplementEntry.ImplementedComment)
                                          .Set(x => x.ImplementedUser, commentImplementEntry.ImplementedUser)
                                            .Set(x => x.ImplementedUserID, commentImplementEntry.ImplementedUserID)
                                              .Set(x => x.ImplementedModuleVersion, commentImplementEntry.ImplementedModuleVersion)
                                         .Set(x => x.ImplementedDateTime, dateTime)
                                         .Set(x => x.Status, CommentStatus.Implemented);

            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Proposes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <param name="dateTime">The date time.</param>
        public async Task ProposeComment(Guid commentID, CommentUserEntry userEntry, DateTime dateTime)
        {
            IMongoCollection<Comment> commentColletion = dbContext.DataBase.GetCollection<Comment>(Constant.CommentCollection);
            var query = Builders<Comment>.Filter.Eq(i => i.CommentID, commentID);
            var update = Builders<Comment>.Update
                                         .Set(x => x.ProposedUser, userEntry.User)
                                         .Set(x => x.ProposedDateTime, dateTime)
                                         .Set(x => x.ProposedUserID, userEntry.UserID)
                                         .Set(x => x.Status, CommentStatus.Proposed);

            await commentColletion.UpdateOneAsync(query, update);
        }

        /// <summary>
        /// Deletes the comment based on module id and topic id
        /// </summary>
        /// <param name="deleteComment"></param>
        public async Task DeleteTopicComments(DeleteComment deleteComment)
        {
            IMongoCollection<CommentEntry> commentColletion = dbContext.DataBase.GetCollection<CommentEntry>(Constant.CommentCollection);
            await commentColletion.DeleteOneAsync(i => i.TopicName == deleteComment.TopicName && i.ModuleID == deleteComment.ModuleID);
        }
    }
}
