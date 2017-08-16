using System.Collections.Generic;
using Api.Models;
using Api.Models.UIComponents;

namespace Api.Repositories {
    public interface IUsersRepository {
        IEnumerable<Posts> GetPostsByUser(int userId, int count = 10, int skip = 0);
        string RegisterProfile(string accessToken, Users user);
        string UploadProfilePicture(ProfileImageComp img);
    }
}