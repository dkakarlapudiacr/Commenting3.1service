using System;
using Xunit;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using Acr.Assist.CommentMicroService.Core.Exception;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class CommentViewServiceUnitTest
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly CommentRepository commentRepository;

        /// <summary>
        /// The comment view repository
        /// </summary>
        private readonly CommentViewRepository commentViewRepository;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly CommentUserEntryValidator commentUserEntryValidator;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly CommentViewService sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewServiceUnitTest"/> class.
        /// </summary>
        public CommentViewServiceUnitTest()
        {
            this.commentRepository = new CommentRepository(Constants.ConnectionString, Constants.DBName); ;
            this.commentViewRepository = new CommentViewRepository(Constants.ConnectionString, Constants.DBName); ;
            this.commentUserEntryValidator = new CommentUserEntryValidator();
            sut = new CommentViewService(commentRepository, commentViewRepository, commentUserEntryValidator);
        }

        /// <summary>
        /// Adds the comment view input validation failure.
        /// </summary>
        [Fact]
        public async Task AddCommentViewInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var userEntry = new CommentUserEntry()
            {
                User = string.Empty,
                UserID = string.Empty
            };
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.AddCommentView(commentID,userEntry));
        }

        /// <summary>
        /// Adds the comment view input resource not found.
        /// </summary>
        [Fact]
        public async Task AddCommentViewInputResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var userEntry = new CommentUserEntry()
            {
                User = "test user",
                UserID = "test userID"
            };
            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.AddCommentView(commentID, userEntry));
        }

        /// <summary>
        /// Deletes the comment view input validation failure.
        /// </summary>
        [Fact]
        public async Task DeleteCommentViewInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var userID = string.Empty;
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.DeleteCommentView(commentID, userID));
        }

        /// <summary>
        /// Deletes the comment view resource not found.
        /// </summary>
        [Fact]
        public async Task DeleteCommentViewResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var userID = "test user ID";
            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.DeleteCommentView(commentID, userID));
        }
    }
}
