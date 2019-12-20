using Acr.Assist.CommentMicroService.Core.DTO;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Service.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitTestData
    {
        public static IEnumerable<object[]> GetCommentFilters()
        {
            yield return new object[] {
                  new List<CommentsFilter>(){
                      new CommentsFilter() { ModuleID = string.Empty, SkipCount = 0, TakeCount = 10, TopicID = "label" },
                      new CommentsFilter() { ModuleID = "B3D715C5-FC49-4BF5-99EA-93D54197295E", SkipCount = 0, TakeCount = 20, TopicID = string.Empty },
                     new CommentsFilter() { ModuleID = "B3D715C5-FC49-4BF5-99EA-93D54197295E", SkipCount = 0, TakeCount = 10, TopicID = "label" }
                  }
                };
        }

        /// <summary>
        /// Gets the add comment entries.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetAddCommentEntries()
        {
            yield return new object[] {
                    new List<AddCommentEntry>(){
                      new AddCommentEntry() {
                          CommentText =string.Empty,
                          CreatedUser =string.Empty,
                          CreatedUserID =string.Empty,
                          ModuleID =string.Empty,
                          ModuleName =string.Empty,
                          ModuleVersion =string.Empty,
                          TopicName = string.Empty
                      },
                        new AddCommentEntry() {
                          CommentText ="Test",
                          CreatedUser =string.Empty,
                          CreatedUserID =string.Empty,
                          ModuleID =string.Empty,
                          ModuleName =string.Empty,
                          ModuleVersion =string.Empty,
                          TopicName = string.Empty
                      }
                    }
                };
        }
    }
}
