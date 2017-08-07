using System.Collections.Generic;
using Api.Models;
using MongoDB.Bson;

namespace Api.Repositories {
    public interface IFollowersRepository {
        bool AddFollower(int followerUserId, int followedUserId);
        BsonElement GetFollowers(int followedUserId, int skip = 0, int count = 20);
        IEnumerable<Users> GetFollowersUserObj(int followedUserId, int skip = 0, int count = 20);
        bool IsFollowingUser(int followerUserId, int followedUserId);
        bool RemoveFollower(int followerUserId, int followedUserId);
    }
}