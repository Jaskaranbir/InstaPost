using Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class FollowersRepository : IFollowersRepository {
        private readonly InstaPostContext db;

        // Fields used
        private static readonly string ID = "_id";
        private static readonly string FOLLOWER_USER_ID = "follower_userId";
        private static readonly string FOLLOWED_USER_ID = "followed_userId";

        public FollowersRepository(InstaPostContext context) {
            this.db = context;
        }

        public bool AddFollower(int followerUserId, int followedUserId) {
            Followers follow = new Followers() {
                FollowerUserId = followerUserId,
                FollowedUserId = followedUserId
            };

            try {
                db.Followers.Add(follow);
                return true;
            }
            catch {
                return false;
            }
        }

        public bool RemoveFollower(int followerUserId, int followedUserId) {
            Followers follow = db.Followers.Remove(e =>
                e.FollowedUserId == followedUserId
                && e.FollowerUserId == followerUserId
            );
            return follow != null;
        }

        public bool IsFollowingUser(int followerUserId, int followedUserId) {
            ProjectionDefinition<Followers> project = Builders<Followers>.Projection.Include(FOLLOWER_USER_ID).Exclude(ID);

            Followers follower = db.Followers
                .Find(e => e.FollowedUserId == followedUserId)
                .Project<Followers>(project).FirstOrDefault();
            return follower != null;
        }

        public BsonElement GetFollowers(int followedUserId, int skip = 0, int count = 20) {
            FilterDefinition<Followers> filter = Builders<Followers>.Filter.Eq(FOLLOWED_USER_ID, followedUserId);

            ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");

            var group = new BsonDocument {
                {"_id", $"${FOLLOWED_USER_ID}"},
                {"followers", new BsonDocument {{"$push", $"${FOLLOWER_USER_ID}"}}}
            };

            return db.Followers.Aggregate()
                .Match(filter)
                .Group(group)
                .Project<BsonDocument>(projection)
                .FirstOrDefault()
                .AsQueryable()
                .Skip(skip)
                .Take(count)
                .FirstOrDefault();
        }

        public IEnumerable<Users> GetFollowersUserObj(int followedUserId, int skip = 0, int count = 20) {
            IEnumerable<Users> ids = GetFollowers(followedUserId, skip, count)
                .Value
                .AsBsonArray
                .Select(e =>
                    db.Users.SingleOrDefault(u => u.UserId == e)
                 );
            return ids;
        }

        public long GetFollowersCount(int followedUserId) {
            long count = db.Followers.Find(e => e.FollowedUserId == followedUserId).Count();
            return count;
        }

        public long GetFollowingCount(int followingUserId) {
            long count = db.Followers.Find(e => e.FollowerUserId == followingUserId).Count();
            return count;
        }
    }
}
