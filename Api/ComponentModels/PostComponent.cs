using Api.Models;
using System;

namespace Api.ComponentModels {

    // Simply contains the data we need to display post. ONLY to display post on frontend.
    // So no unnecessary data is passed over network.
    public class PostComponent {
        public string Image { get; }
        public string Location { get; }
        public string Text { get; }
        public string Username { get; }
        public string Usertag { get; }
        public string UserProfilePic { get; }

        public int PostId { get; }
        public int UserId { get; }
        public int CommentsCount { get; }
        public int LikesCount { get; }

        public string Date { get; }
        public string Time { get; }

        public PostComponent(Posts post) {
            this.Image = post.PostImage;

            Locations location = post.Locations;
            this.Location = location == null
                ? ""
                : $"{location.Address} {location.City}, {location.Country}";

            this.Text = post.PostText;

            Users user = post.User;
            if (user != null) {
                this.Username = user.FirstName +
                                (user.LastName != ""
                                ? $" {user.LastName}"
                                : "");
                this.Usertag = user.Usertag;
                this.UserId = user.UserId;
                this.UserProfilePic = user.ProfilePicture;
            }
            else {
                this.Username = "Dummy Data User";
                this.Usertag = $"Dummy Tag {post.PostId}";
                this.UserId = post.PostId;
                this.UserProfilePic = "/static/default_profile_img.png";
            }

            this.Date = post.PostDate.ToString("dd-MM-yy");
            DateTime time = DateTime.Today.Add(post.PostTime);
            this.Time = time.ToString(@"hh\:mm\:ss tt");

            this.PostId = post.PostId;
            this.CommentsCount = post.CommentsCount;
            this.LikesCount = post.LikesCount;
        }
    }
}
