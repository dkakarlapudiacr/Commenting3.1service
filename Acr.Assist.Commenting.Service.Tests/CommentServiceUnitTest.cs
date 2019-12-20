using System;
using Xunit;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using Acr.Assist.CommentMicroService.Core.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;
using Acr.Assist.CommentMicroService.Core.Integrations;
using ACR.Assist.CommentMicroService.Integrations;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Serilog;
using Microsoft.Extensions.Configuration;
using Acr.Assist.CommentMicroService.Infrastructure;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Email;
using Acr.Assist.Commenting.Infrastructure;
using AutoMapper;
using Acr.Assist.Commenting.Core.Profile;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.SignalR;
using Acr.Assist.CommentMicroService.Core.Integrations.DTO;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class CommentServiceUnitTest
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly CommentRepository commentRepository;

        /// <summary>
        /// The add comment validator
        /// </summary>
        private readonly AddCommentEntryValidator addCommentValidator;

        /// <summary>
        /// The comments filter validator
        /// </summary>
        private readonly CommentsFilterValidator commentsFilterValidator;

        /// <summary>
        /// The comment implement entry validator
        /// </summary>
        private readonly CommentImplementEntryValidator commentImplementEntryValidator;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly CommentUserEntryValidator commentUserEntryValidator;

        /// <summary>
        /// The comment i dvalidator
        /// </summary>
        private readonly CommentIDDetailsValidator commentIDvalidator;

        /// <summary>
        /// The delete comment validator
        /// </summary>
        private readonly DeleteCommentValidator deleteCommentValidator;

        /// <summary>
        /// The email template manager
        /// </summary>
        private readonly IEmailTemplateManager emailTemplateManager;

        /// <summary>
        /// The email notificatio micro service
        /// </summary>
        private readonly IEmailNotificationMicroService emailNotificatioMicroService;

        /// <summary>
        /// The notification sender service
        /// </summary>
        private readonly INotificationSenderService notificationSenderService;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly CommentService sut;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The comment filters
        /// </summary>
        public static IEnumerable<object[]> CommentFilters = UnitTestData.GetCommentFilters();

        /// <summary>
        /// The add comment entries
        /// </summary>
        public static IEnumerable<object[]> AddCommentEntries = UnitTestData.GetAddCommentEntries();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentServiceUnitTest"/> class.
        /// </summary>
        public CommentServiceUnitTest()
        {
            commentRepository = new CommentRepository(Constants.ConnectionString, Constants.DBName);
            addCommentValidator = new AddCommentEntryValidator();
            commentsFilterValidator = new CommentsFilterValidator();
            commentImplementEntryValidator = new CommentImplementEntryValidator();
            commentUserEntryValidator = new CommentUserEntryValidator();
            commentIDvalidator = new CommentIDDetailsValidator();
            deleteCommentValidator = new DeleteCommentValidator();

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();
            configurationManager = new ConfigurationManager(configuration);
            logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            emailTemplateManager = new EmailTemplateManager();
            emailNotificatioMicroService = new EmailNotificationMicroService(configurationManager, logger);
            notificationSenderService = new NotificationSenderService(null);

            var commentEntryProfile = new CommentEntryProfiles();
            var commentImplementProfile = new CommentImplementationProfiles();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(commentEntryProfile);
                cfg.AddProfile(commentImplementProfile);
            });

            mapper = new Mapper(mapperConfig);

            sut = new CommentService(commentRepository, addCommentValidator, commentsFilterValidator,
                commentImplementEntryValidator, commentUserEntryValidator, commentIDvalidator,
                deleteCommentValidator, emailTemplateManager, emailNotificatioMicroService, notificationSenderService, mapper);
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filters">The filters.</param>
        [Theory, MemberData(nameof(CommentFilters))]
        public async Task GetComments(List<CommentsFilter> filters)
        {
            foreach (var filter in filters)
            {
                Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.GetComments(filter));
            }
        }

        /// <summary>
        /// Adds the comment input validation failure.
        /// </summary>
        /// <param name="entries">The entries.</param>
        [Theory]
        [MemberData(nameof(AddCommentEntries))]
        public async Task AddCommentInputValidationFailure(List<AddCommentEntry> entries)
        {
            foreach (var entry in entries)
            {
                Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.AddComment(entry));
            }
        }

        /// <summary>
        /// Deletes the comment input validation failure.
        /// </summary>
        [Fact]
        public async Task DeleteCommentInputValidationFailure()
        {
            var data1 = new CommentIDDetails() { CommentID = Guid.Empty };
            Exception ex1 = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.DeleteComment(data1));
        }

        /// <summary>
        /// Deletes the comment resource not found.
        /// </summary>
        [Fact]
        public async Task DeleteCommentResourceNotFound()
        {
            var data2 = new CommentIDDetails() { CommentID = Guid.NewGuid() };
            Exception ex2 = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.DeleteComment(data2));
        }

        /// <summary>
        /// Edits the comment input validation failure.
        /// </summary>
        [Fact]
        public async Task EditCommentInputValidationFailure()
        {
            Guid commentID1 = Guid.Empty;
            UpdateCommentEntry commentEntry = new UpdateCommentEntry()
            {
                CommentText = "",
                ToRecipients = null
            };
            Exception ex1 = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.EditComment(commentID1, commentEntry));
        }

        /// <summary>
        /// Edits the comment resource not found.
        /// </summary>
        [Fact]
        public async Task EditCommentResourceNotFound()
        {
            Guid commentID2 = Guid.NewGuid();
            UpdateCommentEntry commentEntry = new UpdateCommentEntry()
            {
                CommentText = "",
                ToRecipients = null
            };
            Exception ex2 = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.EditComment(commentID2, commentEntry));
        }

        /// <summary>
        /// Implements the comment input validation exception.
        /// </summary>
        [Fact]
        public async Task ImplementCommentInputValidationException()
        {
            Guid commentID1 = Guid.Empty;
            var entry = new CommentImplementEntry() { ImplementedComment = string.Empty, ImplementedModuleVersion = string.Empty, ImplementedUser = string.Empty, ImplementedUserID = string.Empty };
            Exception ex1 = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.ImplementComment(commentID1, entry));
        }

        /// <summary>
        /// Implements the comment resource not found exception.
        /// </summary>
        [Fact]
        public async Task ImplementCommentResourceNotFoundException()
        {
            Guid commentID2 = Guid.NewGuid();
            var entry = new CommentImplementEntry() { ImplementedComment = "test Comment", ImplementedModuleVersion = "1.0", ImplementedUser = "test User", ImplementedUserID = "1" };
            Exception ex2 = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.ImplementComment(commentID2, entry));
        }

        /// <summary>
        /// Proposes the comment input validation exception.
        /// </summary>
        [Fact]
        public async Task ProposeCommentInputValidationException()
        {
            Guid commentID1 = Guid.Empty;
            var entry = new CommentUserEntry() { User = string.Empty, UserID = string.Empty };
            Exception ex1 = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.ProposeComment(commentID1, entry));
        }

        /// <summary>
        /// Proposes the comment resource not found exception.
        /// </summary>
        [Fact]
        public async Task ProposeCommentResourceNotFoundException()
        {
            Guid commentID2 = Guid.NewGuid();
            var entry = new CommentUserEntry() { User = "test user", UserID = "1" };
            Exception ex2 = await Assert.ThrowsAsync<ResourceNotFoundException>(() => sut.ProposeComment(commentID2, entry));
        }
    }
}
