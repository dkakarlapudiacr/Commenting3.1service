using System;
using Xunit;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using Acr.Assist.CommentMicroService.Core.Exception;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class CommentSuggestionServiceUnitTest
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly CommentRepository commentRepository;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly CommentUserEntryValidator commentUserEntryValidator;

        /// <summary>
        /// The comment suggestion repository
        /// </summary>
        private readonly CommentSuggestionRepository commentSuggestionRepository;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly CommentSuggestionService sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSuggestionServiceUnitTest"/> class.
        /// </summary>
        public CommentSuggestionServiceUnitTest()
        {
            commentRepository = new CommentRepository(Constants.ConnectionString, Constants.DBName);
            commentSuggestionRepository = new CommentSuggestionRepository(Constants.ConnectionString, Constants.DBName);
            commentUserEntryValidator = new CommentUserEntryValidator();
            sut = new CommentSuggestionService(commentRepository, commentSuggestionRepository, commentUserEntryValidator);
        }

        /// <summary>
        /// Adds the comment suggestion input validation failure.
        /// </summary>
        [Fact]
        public async Task AddCommentSuggestionInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var userEntry = new CommentUserEntry() { User = string.Empty, UserID = string.Empty };

            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.AddCommentSuggestion(commentID, userEntry));
        }

        /// <summary>
        /// Adds the comment suggestion resource not found.
        /// </summary>
        [Fact]
        public async Task AddCommentSuggestionResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var userEntry = new CommentUserEntry() { User = "test User", UserID = "test id" };

            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.AddCommentSuggestion(commentID, userEntry));
        }

        /// <summary>
        /// Deletes the comment suggestion input validation failure.
        /// </summary>
        [Fact]
        public async Task DeleteCommentSuggestionInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var userID = string.Empty;
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.DeleteCommentSuggestion(commentID, userID));
        }

        /// <summary>
        /// Deletes the comment suggestion resource not found.
        /// </summary>
        [Fact]
        public async Task DeleteCommentSuggestionResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var userID = "test userID";
            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.DeleteCommentSuggestion(commentID, userID));
        }
    }
}
