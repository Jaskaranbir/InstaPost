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

        public int UserId { get; }
        public int CommentsCount { get; }
        public int LikesCount { get; }

        public DateTime Date { get; }
        public TimeSpan Time { get; }

        public Post(Posts PostObj, Users UserObj, Locations LocationObj)
        {
            this.Image = PostObj.PostImage;

            this.Location = $"{LocationObj.Address} {LocationObj.City}, {LocationObj.Country}";

            this.Text = PostObj.PostText;

            this.Username = UserObj.FirstName +
                            (UserObj.LastName != ""
                            ? $" {UserObj.LastName}"
                            : "");

            this.Usertag = UserObj.Username;

            this.Date = PostObj.PostDate;
            this.Time = PostObj.PostTime;

            this.UserId = UserObj.UserId;
            this.CommentsCount = PostObj.CommentsCount;
            this.LikesCount = PostObj.LikesCount;
        }
    }
}
