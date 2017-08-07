using Api.Models;
using Api.MongoWrappers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class LikesRepository : ILikesRepository {
        private readonly InstaPostContext db;

        // Fields Used
        private static readonly string USER_IDS = "l_userIds";

        public LikesRepository(InstaPostContext context) {
            this.db = context;
        }

        public Task<Likes> AddLike(int postId, params int[] userIds) {
            Likes like = new Likes() {
                PostId = postId,
                UserIds = userIds
            };
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

            bool isEntityPresent = db.Likes.CheckAndCreateEntityBool(like, filter);
            if (isEntityPresent) {
                Task<Likes> task = MongoArrayUtils<Likes>.AddToArray<int>(db.Likes, filter, USER_IDS, userIds);
                return task;
            }
            return null;
        }

        public Task<Likes> RemoveLike(int postId, params int[] userIds) {
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

            Task<Likes> task = MongoArrayUtils<Likes>.RemoveFromArray(db.Likes, filter, USER_IDS, userIds);
            return task;
        }

        public IEnumerable<int> GetAllLikes(int postId) {
            return GetLikesInRange(postId, 0);
        }

        public IEnumerable<Users> GetAllLikesUsers(int postId) {
            return GetLikesUsersInRange(postId, 0);
        }

        public IEnumerable<int> GetLikesInRange(int postId, int count, int skip = 0) {
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

            IEnumerable<int> array = MongoArrayUtils<Likes>.ArrayIntSplice(db.Likes, USER_IDS, filter, count, skip);
            return array;
        }

        public IEnumerable<Users> GetLikesUsersInRange(int postId, int count, int skip = 0) {
            IEnumerable<Users> users = GetLikesInRange(postId, count, skip)
                .Select(e =>
                    db.Users.SingleOrDefault(u => u.UserId == e)
                );

            return users;
        }

        private FilterDefinition<Likes> GetPostIdFilter(int postId) {
            return Builders<Likes>.Filter.Where(e => e.PostId == postId);
        }
    }
}
