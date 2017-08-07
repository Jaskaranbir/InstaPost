using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using MongoDB.Driver;

namespace Api.Repositories {
    public interface ITagsRepository {
        Task<Tags>[] AddPostTags(string[] tagTexts, int postId);
        long GetPostCountByTag(string tagText);
        IEnumerable<int> GetPostsByTag(string tagText, int count = 10, int skip = 0);
        IEnumerable<Posts> GetPostsObjByTag(string tagText, int count = 10, int skip = 0);
        FilterDefinition<Tags> GetTagTextFilter(string tagText);
        Task<Tags> RemoveTag(string tagText, int postId);
    }
}