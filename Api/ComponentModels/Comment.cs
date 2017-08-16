using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ComponentModels
{
    // Simply contains the data we need to display a Comment. ONLY to display post on frontend.
    // So no unnecessary data is passed over network.
    public class Comment
    {
        public int CommentId { get; }
        public int PostId { get; }
        public int UserId { get; }
        public int? ParentCommentId { get; }

        public string CommentText { get; }
        public string Username { get; }
        public string Usertag { get; }
        
        public DateTime CommentDate { get; }
        public TimeSpan CommentTime { get; }

        public Comment(Comments comment) {
            this.CommentId = comment.CommentId;
            this.PostId = comment.PostId;
            this.UserId = comment.UserId;
            this.ParentCommentId = comment.ParentCommentId;

            this.CommentText = comment.CommentText;

            Users user = comment.User;
            this.Username = user.FirstName +
                            (user.LastName != ""
                            ? $" {user.LastName}"
                            : "");

            this.Usertag = user.Usertag;

            this.CommentDate = comment.CommentDate;
            this.CommentTime = comment.CommentTime;
        }
    }
}
