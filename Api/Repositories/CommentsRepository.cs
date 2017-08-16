using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class CommentsRepository : ICommentsRepository {
        private readonly InstaPostContext db;

        public CommentsRepository(InstaPostContext context) {
            this.db = context;
        }

		//Adds comment to Comments Table
        public Comments AddComment(Comments comment) {
            db.Comments.Add(comment);
            db.SaveChanges();
            return comment;
        }

		//Updates a comment in the Comments Table
        public Comments UpdateComment(int commentId, string commentText) {
            Comments comment = db.Comments.SingleOrDefault(e => e.CommentId == commentId);

            comment.CommentText = commentText;
            db.Update(comment);
            db.SaveChanges();
            return comment;
        }

		//Removes a comment from the Comments Table
        public Comments RemoveComment(int commentId) {
            Comments comment = new Comments() {
                CommentId = commentId,
            };

            db.Comments.Attach(comment);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return comment;
        }

		//Returns a collection of comments associated with a post
        public IEnumerable<Comments> LoadInitial(int postId) {
            return LoadRange(postId);
        }

		//Returns a collection in order of comments posted last, associated with a post 
        public IEnumerable<Comments> LoadRange(int postId, int count = 10, int skip = 0, bool isNewerFirst = true) {
            IQueryable<Comments> comments = db.Comments
                .Where(e => e.PostId == postId);

            if(isNewerFirst)
                comments = comments
                    .OrderByDescending(e => e.CommentDate)
                    .OrderByDescending(e => e.CommentTime);

            return comments
                    .Skip(skip)
                    .Take(count)
                    .AsEnumerable();
        }

		//Returns a collection of comments that were posted from most recently 
        public IEnumerable<Comments> GetLatest(int postId, int lastCommentId, int limit) {
            IEnumerable<Comments> comments = db.Comments
                .Where(e => e.PostId == postId && e.CommentId > lastCommentId)
                .OrderByDescending(e => e.CommentDate)
                .OrderByDescending(e => e.CommentTime)
                .Take(limit)
                .AsEnumerable<Comments>();
            return comments;
        }
    }
}
