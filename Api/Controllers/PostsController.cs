using Api.Models;
using Api.ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Api.Repositories;
using Api.Models.UIComponents;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers {
    [Route("api/[controller]")]
    public class PostsController : Controller {

        private readonly InstaPostContext db;
        private readonly IPostsRepository postRepo;
        private readonly ILikesRepository likesRepo;

        public PostsController(
            InstaPostContext context,
            IPostsRepository postRepo,
            ILikesRepository likesRepo
        ) {
            db = context;
            this.postRepo = postRepo;
            this.likesRepo = likesRepo;
        }

        [HttpGet("initial")]
        public string Initial() {
            return ToJson(
                postRepo
                .LoadInitial()
                .Select(e => new PostComponent(e))
            );
        }

        [HttpGet("next")]
        public string Next([FromQuery]int lastPostId) {
            IEnumerable<Posts> posts = postRepo.GetLatest(lastPostId, 10);
            if(posts.LastOrDefault() == null)
                return "";

            return ToJson(
                postRepo
                .GetLatest(lastPostId, 10)
                .Select(e => new PostComponent(e))
            );
        }

        [HttpPost]
        [Authorize]
        public string Posted([FromBody]Posts post) {
            if (post == null) return "nulled";
            Posts newPost = postRepo.AddPost(new Posts() {
                UserId = post.UserId,
                Locations = post.Locations,
                PostText = post.PostText,
                PostDate = post.PostDate,
                PostTime = post.PostTime,
                PostImage = post.PostImage
            });
            return ToJson(new PostComponent(newPost));
        }

        [HttpPost("profileimg")]
        [Authorize]
        public string PostImage([FromBody]ProfileImageComp img) {
            return postRepo.UploadImage(img);
        }

        [HttpGet("byuser")]
        public string PostsByUser([FromQuery] int userId) {
            return ToJson(
                postRepo
                .GetPostsByUser(userId, 10)
                .Select(e => new PostComponent(e))
            );
        }

        [HttpPut("add-like")]
        [Authorize]
        public void LikePost([FromQuery]int postId, int userId) {
            likesRepo.AddLike(postId, userId);
        }

        private string ToJson(object o) {
            return JsonConvert.SerializeObject(o);
        }
    }
}
