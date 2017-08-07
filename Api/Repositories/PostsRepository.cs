using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class PostsRepository : IPostsRepository {
        private readonly InstaPostContext db;

        public PostsRepository(InstaPostContext context) {
            this.db = context;
        }

        public Posts AddPost(Posts post) {
            db.Posts.Add(post);
            db.SaveChanges();
            return post;
        }

        public Posts UpdatePost(int postId, string postText, string postImage) {
            Posts post = db.Posts.SingleOrDefault(e => e.PostId == postId);

            post.PostText = postText;
            post.PostImage = postImage;
            db.Update(post);
            db.SaveChanges();
            return post;
        }

        public Posts RemovePost(int postId) {
            Posts post = new Posts() {
                PostId = postId,
            };

            db.Posts.Attach(post);
            db.Posts.Remove(post);
            db.SaveChanges();
            return post;
        }

        public IEnumerable<Posts> LoadInitial() {
            return LoadRange();
        }

        public IEnumerable<Posts> LoadRange(int count = 10, int skip = 0, bool isNewerFirst = true) {
            IQueryable<Posts> posts = db.Posts;

            if (isNewerFirst)
                posts = posts
                    .OrderByDescending(e => e.PostDate)
                    .OrderByDescending(e => e.PostTime);

            return posts
                    .Skip(skip)
                    .Take(count)
                    .AsEnumerable();
        }

        public IEnumerable<Posts> GetLatest(int lastPostId, int limit) {
            IEnumerable<Posts> posts = db.Posts
                .Where(e => e.PostId > lastPostId)
                .OrderByDescending(e => e.PostDate)
                .OrderByDescending(e => e.PostTime)
                .Take(limit)
                .AsEnumerable<Posts>();
            return posts;
        }
    }
}
