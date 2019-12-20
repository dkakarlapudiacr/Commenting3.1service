using Acr.Assist.Commenting.Core.Profile;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class CommentNotificationServiceUnitTest
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly CommentRepository commentRepository;

        /// <summary>
        /// The comment notification repository
        /// </summary>
        private readonly CommentNotificationRepository commentNotificationRepository;

        /// <summary>
        /// The comment notification entry validator
        /// </summary>
        private readonly CommentNotificationEntryValidator commentNotificationEntryValidator;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly CommentNotificationService sut;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotificationServiceUnitTest"/> class.
        /// </summary>
        public CommentNotificationServiceUnitTest()
        {
            commentRepository = new CommentRepository(Constants.ConnectionString, Constants.DBName);
            commentNotificationRepository = new CommentNotificationRepository(Constants.ConnectionString, Constants.DBName);
            commentNotificationEntryValidator = new CommentNotificationEntryValidator();

            var commentNotificationProfile = new CommentNotificationProfiles();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(commentNotificationProfile));
            mapper = new Mapper(mapperConfig);

            sut = new CommentNotificationService(commentRepository, commentNotificationRepository, commentNotificationEntryValidator, mapper);
        }

        /// <summary>
        /// Adds the comment notification input validation failure.
        /// </summary>
        [Fact]
        public async Task AddCommentNotificationInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var data = new CommentNotificationEntry() { EmailID = string.Empty, User = string.Empty, UserID = string.Empty };
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.AddCommentNotification(commentID, data));
        }

        /// <summary>
        /// Adds the comment notification resource not found.
        /// </summary>
        [Fact]
        public async Task AddCommentNotificationResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var data = new CommentNotificationEntry() { EmailID = "abc@abc.com", User = "test user", UserID = "test user ID" };
            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.AddCommentNotification(commentID, data));
        }

        /// <summary>
        /// Deletes the comment notification input validation failure.
        /// </summary>
        [Fact]
        public async Task DeleteCommentNotificationInputValidationFailure()
        {
            var commentID = Guid.Empty;
            var userID = string.Empty;
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.DeleteCommentNotification(commentID, userID));
        }

        /// <summary>
        /// Deletes the comment notification resource not found.
        /// </summary>
        [Fact]
        public async Task DeleteCommentNotificationResourceNotFound()
        {
            var commentID = Guid.NewGuid();
            var userID = "test userID";
            Exception ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.DeleteCommentNotification(commentID, userID));
        }
    }
}
