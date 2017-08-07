using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories {
    public interface IReportsRepository {
        int GetPostReportedCount(int postId);
        Task<Reports> AddReportPost(int postId, int userId, string comment);
        IEnumerable<Posts> GetReportedPostObj(int skip = 0, int count = 10);
        IEnumerable<int> GetReportedPosts(int skip = 0, int count = 10);
        Task<Reports> SetResolved(int postId, int userId);
    }
}