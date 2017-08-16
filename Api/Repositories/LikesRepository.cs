using Api.Models;
using Api.MongoWrappers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class LikesRepository : ILikesRepository {
        private readonly InstaPostContext db;
        private readonly IPostsRepository postRepo;

        // Fields Used
        private static readonly string USER_IDS = "l_userIds";
<<<<<<< Updated upstream

        public LikesRepository(InstaPostContext context, IPostsRepository postRepo) {
=======
		
		
        public LikesRepository(InstaPostContext context) {
>>>>>>> Stashed changes
            this.db = context;
            this.postRepo = postRepo;
        }

		//Add a user's like to a Post
        public Task<Likes> AddLike(int postId, params int[] userIds) {
            Likes like = new Likes() {
                PostId = postId,
                UserIds = userIds
            };
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

			//Checks to see if an instance of Like is available
            bool isEntityPresent = db.Likes.CheckAndCreateEntityBool(like, filter);
            if (isEntityPresent) {
                Task<Likes> task = MongoArrayUtils<Likes>.AddToArray<int>(db.Likes, filter, USER_IDS, userIds);

                int likesCount = db.Posts.FirstOrDefault(e => e.PostId == postId).LikesCount;
                postRepo.UpdatePostLikesCount(postId);
                return task;
            }
            return null;
        }

		//Remove a user's like from a post
        public Task<Likes> RemoveLike(int postId, params int[] userIds) {
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

            Task<Likes> task = MongoArrayUtils<Likes>.RemoveFromArray(db.Likes, filter, USER_IDS, userIds);
            return task;
        }

		//return the total amount of likes a post has
        public IEnumerable<int> GetAllLikes(int postId) {
            return GetLikesInRange(postId, 0);
        }

		//Returns a collection of users who have liked a post
        public IEnumerable<Users> GetAllLikesUsers(int postId) {
            return GetLikesUsersInRange(postId, 0);
        }

		//Returns array
        public IEnumerable<int> GetLikesInRange(int postId, int count, int skip = 0) {
            FilterDefinition<Likes> filter = GetPostIdFilter(postId);

            IEnumerable<int> array = MongoArrayUtils<Likes>.ArrayIntSplice(db.Likes, USER_IDS, filter, count, skip);
            return array;
        }

		//Returns a collection of users who have liked a post
        public IEnumerable<Users> GetLikesUsersInRange(int postId, int count, int skip = 0) {
            IEnumerable<Users> users = GetLikesInRange(postId, count, skip)
                .Select(e =>
                    db.Users.SingleOrDefault(u => u.UserId == e)
                );

            return users;
        }

		//Returns filter used to find post(s)
        private FilterDefinition<Likes> GetPostIdFilter(int postId) {
            return Builders<Likes>.Filter.Where(e => e.PostId == postId);
        }
    }
}
