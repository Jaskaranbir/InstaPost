using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class UsersRepository
    {
        private readonly InstaPostContext db;

        public UsersRepository(InstaPostContext context) {
            this.db = context;
        }
        
        public IEnumerable<Posts> GetPostsByUser(int userId, int count = 10, int skip = 0) {
            IEnumerable<Posts> posts = db.Posts
                .Where(e => e.UserId == userId)
                .Skip(skip)
                .Take(count)
                .AsEnumerable<Posts>();
            return posts;
        }
    }
}
