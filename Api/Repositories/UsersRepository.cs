using Api.ComponentModels;
using Api.Models;
using Api.Models.Config;
using Api.Models.UIComponents;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class UsersRepository : IUsersRepository {
        private readonly InstaPostContext db;
        private readonly Auth0Config authConfig;
        private readonly Cloudinary imgCloud;

        public UsersRepository(
            InstaPostContext context,
            IOptions<Auth0Config> authConfig,
            IOptions<CloudinaryConfig> cloudinaryConfig
        ) {
            this.db = context;
            this.authConfig = authConfig.Value;

            CloudinaryConfig cloudConfig = cloudinaryConfig.Value;
            Account account = new Account(
                cloudConfig.CloudName,
                cloudConfig.Key,
                cloudConfig.Secret
            );
            Cloudinary cloudinary = new Cloudinary(account);
            this.imgCloud = cloudinary;
        }

		//returns a collection of posts that a user has posted, based on userID
        public IEnumerable<Posts> GetPostsByUser(int userId, int count = 10, int skip = 0) {
            IEnumerable<Posts> posts = db.Posts
                .Where(e => e.UserId == userId)
                .Skip(skip)
                .Take(count)
                .AsEnumerable<Posts>();
            return posts;
        }

        public string UploadProfilePicture(ProfileImageComp img) {
            ImageUploadParams uploadParams = new ImageUploadParams() {
                File = new FileDescription(@img.Base64ImageData),
                Transformation = new Transformation()
                                 .Width(256)
                                 .Height(256)
            };
            return imgCloud.UploadAsync(uploadParams).Result.Uri.ToString();
        }

        public string RegisterProfile(string accessToken, Users user) {
            string authDomain = authConfig.Domain;
            AuthenticationApiClient authClient = new AuthenticationApiClient(authDomain);

            UserInfo userInfo = authClient.GetUserInfoAsync(accessToken).Result;

            user.IsSuspended = false;
            user.Usertag = userInfo.NickName;

            Users dbuser = db.Users.FirstOrDefault(e => e.Usertag == user.Usertag);
            if (dbuser == null) {
                db.Attach<Users>(user);
                db.Add<Users>(new Users() {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Usertag = user.Usertag,
                    ProfilePicture = user.ProfilePicture,
                    IsSuspended = false
                });
                db.SaveChanges();
            }
            else {
                return ToJson(new UserComponent(dbuser));
            }

            ConfigureManagementApi(user, userInfo);
            return ToJson(new UserComponent(dbuser));
        }

        private void ConfigureManagementApi(Users user, UserInfo authUserInfo) {
            var client = new RestClient($"https://{authConfig.Domain}/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");

            dynamic requestBody = new ExpandoObject();
            requestBody.client_id = authConfig.PrivateClientId;
            requestBody.client_secret = authConfig.PrivateClientSecret;
            requestBody.audience = $"{authConfig.Domain}/api/v2/";
            requestBody.grant_type = "client_credentials";

            request.AddParameter(
                "application/json",
                ToJson(requestBody),
                ParameterType.RequestBody
            );

            client.ExecuteAsync(request, res => {
                string key = JsonConvert.DeserializeObject<AccessTokens>(res.Content).AccessToken;
                SetInitialMetaData(res.Content, authUserInfo.UserId, user.UserId);
            });
        }

        private void SetInitialMetaData(string key, string userAuthId, int userId) {
            string key2 = JsonConvert.DeserializeObject<AccessTokens>(key).AccessToken;

            var apiClient2 = new ManagementApiClient(key2, authConfig.Domain);

            dynamic appMetadata = new ExpandoObject();
            appMetadata.userId = userId;

            apiClient2.Users.UpdateAsync(userAuthId, new Auth0.ManagementApi.Models.UserUpdateRequest() {
                AppMetadata = appMetadata,
                EmailVerified = true
            });
        }

        public Users UpdateUser(Users user) {
            db.Attach<Users>(user);
            db.Update<Users>(user);
            db.SaveChanges();
            return user;
        }

        private string ToJson(object o) {
            return JsonConvert.SerializeObject(o);
        }
    }
}
