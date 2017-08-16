using Api.Models;
using Api.MongoWrappers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class BookmarksRepository : IBookmarksRepository {
        private readonly InstaPostContext db;

        // Fields used
        private static readonly string POST_IDS = "b_postIds";
        private static readonly string COUNT = "b_count";

        public BookmarksRepository(InstaPostContext context) {
            this.db = context;
        }
    
		//Add bookmarks to Bookmarks Table
        public Task<Bookmarks> AddBookmarks(int userId, params int[] postIds) {
            Bookmarks bookmark = new Bookmarks() {
                UserId = userId,
                PostIds = postIds
            };
            FilterDefinition<Bookmarks> filter = GetUserIdFilter(userId);

            bool isEntityPresent = db.Bookmarks.CheckAndCreateEntityBool(bookmark, filter);

            if (isEntityPresent) {
                int[] initialValues = db.Bookmarks.Find(filter).FirstOrDefault().PostIds;

                Task<Bookmarks> updateTask = MongoArrayUtils<Bookmarks>.AddToArrayWithCount(db.Bookmarks, filter, POST_IDS, postIds, initialValues, COUNT);
            }
            return null;
        }

		//Remove Bookmarks from Bookmarks Table
        public Task<Bookmarks> RemoveBookmarks(int userId, params int[] postIds) {
            FilterDefinition<Bookmarks> filter = GetUserIdFilter(userId);
            Task<Bookmarks> removeTask = MongoArrayUtils<Bookmarks>.RemoveFromArray<int>(db.Bookmarks, filter, POST_IDS, postIds);

            return removeTask;
        }

		//returns a collection of bookmarks for a user
        public Task<Bookmarks> UpdateBookmark(int userId, int oldPostId, int newPostId) {
            FilterDefinition<Bookmarks> filter = GetUserIdFilter(userId);

            Task<Bookmarks> task = MongoArrayUtils<Bookmarks>.UpdateInArray<int>(db.Bookmarks, filter, POST_IDS, oldPostId, newPostId);

            return task;
        }
	
		//returns the amount of bookmarks a user has
        public int GetBookmarksCount(int userId) {
            int count = db.Bookmarks.Where(e => e.UserId == userId).FirstOrDefault().Count;
            return count;
        }

		//returns a collection of the bookmarks, by bookmarkID, a user has 
        public IEnumerable<int> GetAllBookmarks(int userId) {
            return GetBookmarksInRange(userId, 0);
        }

		//returns a collection of Posts that have the same bookmark
        public IEnumerable<Posts> GetAllBookmarksPosts(int userId) {
            return GetBookmarksPostsInRange(userId, 0);
        }

        public IEnumerable<int> GetBookmarksInRange(int userId, int count = 10, int skip = 0) {
            FilterDefinition<Bookmarks> filter = GetUserIdFilter(userId);

            IEnumerable<int> array = MongoArrayUtils<Bookmarks>.ArrayIntSplice(db.Bookmarks, POST_IDS, filter, count, skip);
            return array;
        }

        public IEnumerable<Posts> GetBookmarksPostsInRange(int userId, int count = 10, int skip = 0) {
            IEnumerable<Posts> posts = GetBookmarksInRange(userId, count, skip)
                .Select(e =>
                    db.Posts.SingleOrDefault(p => p.PostId == e)
                );
                        
            return posts;
        }
        
        private FilterDefinition<Bookmarks> GetUserIdFilter(int userId) {
            return Builders<Bookmarks>.Filter.Where(e => e.UserId == userId);
        }
    }
}
