using System.Collections.Generic;
using Api.Models;

namespace Api.Repositories {
    public interface IPostsRepository {
        Posts AddPost(Posts post);
        IEnumerable<Posts> GetLatest(int lastPostId, int limit);
        IEnumerable<Posts> LoadInitial();
        IEnumerable<Posts> LoadRange(int count = 10, int skip = 0, bool isNewerFirst = true);
        Posts RemovePost(int postId);
        Posts UpdatePost(int postId, string postText, string postImage);
    }
}