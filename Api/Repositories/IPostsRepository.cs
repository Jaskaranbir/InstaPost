using System.Collections.Generic;
using Api.Models;
using Api.Models.UIComponents;

namespace Api.Repositories {
    public interface IPostsRepository {
        Posts AddPost(Posts post);
        IEnumerable<Posts> GetLatest(int lastPostId, int limit);
        IEnumerable<Posts> LoadInitial();
        IEnumerable<Posts> LoadRange(int count = 10, int skip = 0, bool isNewerFirst = true);
        Posts RemovePost(int postId);
        Posts UpdatePost(int postId, string postText, string postImage);
        Posts UpdatePostLikesCount(int postId);
        string UploadImage(ProfileImageComp img);
        IEnumerable<Posts> GetPostsByUser(int userId, int count = 10, int skip = 0);
        long GetPostsByUserCount(int userId);
    }
}