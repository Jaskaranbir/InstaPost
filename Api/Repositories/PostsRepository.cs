using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.UIComponents;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using Api.Models.Config;
using CloudinaryDotNet.Actions;

namespace Api.Repositories
{
    public class PostsRepository : IPostsRepository {
        private readonly InstaPostContext db;
        private readonly Cloudinary imgCloud;

        public PostsRepository(
            InstaPostContext context,
            IOptions<CloudinaryConfig> cloudinaryConfig
        ) {
            this.db = context;

            CloudinaryConfig cloudConfig = cloudinaryConfig.Value;
            Account account = new Account(
                cloudConfig.CloudName,
                cloudConfig.Key,
                cloudConfig.Secret
            );
            Cloudinary cloudinary = new Cloudinary(account);
            this.imgCloud = cloudinary;
        }
		
		//Adds new post to Posts Table
        public Posts AddPost(Posts post) {
            db.Posts.Add(post);
            db.SaveChanges();
            return post;
        }

		//Updates a Post in Posts Table
        public Posts UpdatePost(int postId, string postText, string postImage) {
            Posts post = db.Posts.SingleOrDefault(e => e.PostId == postId);

            post.PostText = postText;
            post.PostImage = postImage;
            db.Update(post);
            db.SaveChanges();
            return post;
        }
        
        public Posts UpdatePostLikesCount(int postId) {
            Posts post = db.Posts.SingleOrDefault(e => e.PostId == postId);
            post.LikesCount++;
            db.Update(post);
            db.SaveChanges();
            return post;
        }
        
		//Removes Post from Posts Table
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

		//Returns collection of Posts In order of Post date and then Post time to show most recent post first
        public IEnumerable<Posts> LoadRange(int count = 10, int skip = 0, bool isNewerFirst = true) {
            IQueryable<Posts> posts = db.Posts;

            if (isNewerFirst)
                posts = posts
                    .OrderByDescending(e => e.PostId);

            return posts
                    .Skip(skip)
                    .Take(count)
                    .Include(e => e.User)
                    .AsEnumerable();
        }

		//Returns new posts that were added last to the Posts Table
        public IEnumerable<Posts> GetLatest(int lastPostId, int limit) {
            IEnumerable<Posts> posts = db.Posts
                .Where(e => e.PostId > lastPostId)
                .Include(e => e.User)
                .OrderByDescending(e => e.PostDate)
                .OrderByDescending(e => e.PostTime)
                .Take(limit)
                .AsEnumerable<Posts>();
            return posts;
        }

        public string UploadImage(ProfileImageComp img) {
            ImageUploadParams uploadParams = new ImageUploadParams() {
                File = new FileDescription(@img.Base64ImageData),
                Transformation = new Transformation()
                                 .Width(512)
                                 .Height(512)
            };
            return imgCloud.UploadAsync(uploadParams).Result.Uri.ToString();
        }

        public IEnumerable<Posts> GetPostsByUser(
            int userId,
            int count = 10,
            int skip = 0
        ) {
            IQueryable<Posts> qposts = db.Posts
                .Where(e => e.UserId == userId)
                .Skip(skip)
                .Take(count)
                .Include(e => e.User);

            return qposts.AsEnumerable<Posts>();
        }

        public long GetPostsByUserCount(int userId) {
            IQueryable<Posts> qposts = db.Posts
                .Where(e => e.UserId == userId);

            return qposts.LongCount();
        }
    }
}
