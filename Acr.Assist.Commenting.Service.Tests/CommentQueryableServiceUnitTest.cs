using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Service.Validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class CommentQueryableServiceUnitTest
    {
        /// <summary>
        /// The comment queryable repository
        /// </summary>
        private readonly CommentQueryableRepository commentQueryableRepository;

        /// <summary>
        /// The comments filter validator
        /// </summary>
        private readonly CommentsFilterValidator commentsFilterValidator;

        /// <summary>
        /// The sut
        /// </summary>
        private readonly CommentQueryableService sut;

        /// <summary>
        /// The comment filters
        /// </summary>
        public static IEnumerable<object[]> CommentFilters = UnitTestData.GetCommentFilters();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentQueryableServiceUnitTest"/> class.
        /// </summary>
        public CommentQueryableServiceUnitTest()
        {
            commentQueryableRepository = new CommentQueryableRepository(Constants.ConnectionString, Constants.DBName);
            commentsFilterValidator = new CommentsFilterValidator();
            sut = new CommentQueryableService(commentQueryableRepository, commentsFilterValidator);            
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
    }
}
