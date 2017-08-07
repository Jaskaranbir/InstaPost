using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories {
    public interface ILikesRepository {
        Task<Likes> AddLike(int postId, params int[] userIds);
        IEnumerable<int> GetAllLikes(int postId);
        IEnumerable<Users> GetAllLikesUsers(int postId);
        IEnumerable<int> GetLikesInRange(int postId, int count, int skip = 0);
        IEnumerable<Users> GetLikesUsersInRange(int postId, int count, int skip = 0);
        Task<Likes> RemoveLike(int postId, params int[] userIds);
    }
}