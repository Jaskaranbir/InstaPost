using Api.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories {
    public interface IBookmarksRepository {
        Task<Bookmarks> AddBookmarks(int userId, params int[] postIds);
        IEnumerable<int> GetAllBookmarks(int userId);
        IEnumerable<Posts> GetAllBookmarksPosts(int userId);
        int GetBookmarksCount(int userId);
        IEnumerable<int> GetBookmarksInRange(int userId, int count, int skip = 0);
        IEnumerable<Posts> GetBookmarksPostsInRange(int userId, int count, int skip = 0);
        Task<Bookmarks> RemoveBookmarks(int userId, params int[] postIds);
        Task<Bookmarks> UpdateBookmark(int userId, int oldPostId, int newPostId);
    }
}