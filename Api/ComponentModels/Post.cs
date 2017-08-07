using Api.Models;
using System;

namespace Api.ComponentModels
{

    // Simply contains the data we need to display post. ONLY to display post on frontend.
    // So no unnecessary data is passed over network.
    public class Post
    {
        public string Image { get; }
        public string Location { get; }
        public string Text { get; }
        public string Username { get; }
        public string Usertag { get; }

        public int PostId { get; }
        public int UserId { get; }
        public int CommentsCount { get; }
        public int LikesCount { get; }

        public DateTime Date { get; }
        public TimeSpan Time { get; }

        public Post(Posts post)
        {
            this.Image = post.PostImage;

            Locations location = post.Locations;
            this.Location = location == null
                ? ""
                : $"{location.Address} {location.City}, {location.Country}";

            this.Text = post.PostText;

            Users user = post.User;
            this.Username = user.FirstName +
                            (user.LastName != ""
                            ? $" {user.LastName}"
                            : "");

            this.Usertag = user.Usertag;

            this.Date = post.PostDate;
            this.Time = post.PostTime;

            this.PostId = post.PostId;
            this.UserId = user.UserId;
            this.CommentsCount = post.CommentsCount;
            this.LikesCount = post.LikesCount;
        }
    }
}
