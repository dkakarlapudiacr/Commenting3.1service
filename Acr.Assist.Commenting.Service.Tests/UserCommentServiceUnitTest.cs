using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class UserCommentServiceUnitTest
    {
        /// <summary>
        /// The user comment repository
        /// </summary>
        private readonly UserCommentRepository userCommentRepository;

        /// <summary>
        /// The user comments validator
        /// </summary>
        private readonly UserCommentsViewEntryValidator userCommentsValidator;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly UserCommentService sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentServiceUnitTest"/> class.
        /// </summary>
        public UserCommentServiceUnitTest()
        {
            userCommentRepository = new UserCommentRepository(Constants.ConnectionString, Constants.DBName);
            userCommentsValidator = new UserCommentsViewEntryValidator();
            sut = new UserCommentService(userCommentRepository, userCommentsValidator);
        }

        [Fact]
        public async Task GetUnViwedComment()
        {
            var userID = string.Empty;

            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.GetUnViwedComment(userID));
        }

        /// <summary>
        /// Updates the comments as viewed.
        /// </summary>
        [Fact]
        public async Task UpdateCommentsAsViewed()
        {
            var data = new UserCommentsViewEntry() { ModuleID = string.Empty, TopicID = string.Empty, UserID = string.Empty, UserName = string.Empty };

            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.UpdateCommentsAsViewed(data));
        }
    }
}
