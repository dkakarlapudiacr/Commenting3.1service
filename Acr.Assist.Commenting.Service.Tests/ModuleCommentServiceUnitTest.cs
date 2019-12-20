using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Data;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    public class ModuleCommentServiceUnitTest
    {
        private readonly ModuleCommentRepository moduleCommentRepository;
        private readonly ModuleCommentService sut;

        public ModuleCommentServiceUnitTest()
        {
            moduleCommentRepository = new ModuleCommentRepository(Constants.ConnectionString, Constants.DBName);
            sut = new ModuleCommentService(moduleCommentRepository);
        }

        [Fact]
        public async Task GetCommentDetails()
        {
            var moduleID = string.Empty;
            Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.GetCommentDetails(moduleID));
        }
    }
}
