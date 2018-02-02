using Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Auth0.AuthenticationApi;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using System.Dynamic;
using Auth0.AuthenticationApi.Models;
using RestSharp;
using Api.Repositories;
using Api.Models.UIComponents;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Api.ComponentModels;

namespace Api.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller {
        private readonly InstaPostContext db;
        private readonly IUsersRepository userRepo;
        private readonly IFollowersRepository followerRepo;
        private readonly IPostsRepository postRepo;

        public UsersController(
            InstaPostContext context,
            IUsersRepository userRepo,
            IFollowersRepository followerRepo,
            IPostsRepository postRepo
        ) {
            db = context;
            this.userRepo = userRepo;
            this.followerRepo = followerRepo;
            this.postRepo = postRepo;
        }

        [HttpPost]
        public string Post([FromBody]Users user) {
            string accessToken = User.Claims.FirstOrDefault(c => c.Type == "access_token").Value;
            string userInfo = userRepo.RegisterProfile(accessToken, user);
            return userInfo;
        }

        [HttpPost("profileimg")]
        public string ProfileImgUpload([FromBody]ProfileImageComp img) {
            return userRepo.UploadProfilePicture(img);
        }

        [HttpGet("userMeta")]
        public string GetUserMeta(int userId) {
            UserMetaComp userm = new UserMetaComp();
            userm.Followers = followerRepo.GetFollowersCount(userId);
            userm.Followings = followerRepo.GetFollowingCount(userId);
            userm.PostCount = postRepo.GetPostsByUserCount(userId);
            return ToJson(userm);
        }

        [HttpGet("followers")]
        public long FollowersCount([FromQuery]int userId) {
            return followerRepo.GetFollowersCount(userId);
        }

        [HttpGet("followings")]
        public long FollowingCount([FromQuery]int userId) {
            return followerRepo.GetFollowingCount(userId);
        }

        [HttpPost("update")]
        public string UpdateUser([FromBody]Users user) {
            Users newUser = userRepo.UpdateUser(user);
            return ToJson(new UserComponent(newUser));
        }

        private string ToJson(object o) {
            return JsonConvert.SerializeObject(o);
        }
    }
}
