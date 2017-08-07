using System.Collections.Generic;
using Api.Models;
using System.Linq;

namespace Api.Repositories {
    public interface ICommentsRepository {
        Comments AddComment(Comments comment);
        IEnumerable<Comments> GetLatest(int postId, int lastCommentId, int limit);
        IEnumerable<Comments> LoadInitial(int postId);
        IEnumerable<Comments> LoadRange(int postId, int count = 10, int skip = 0, bool isNewerFirst = true);
        Comments RemoveComment(int commentId);
        Comments UpdateComment(int commentId, string commentText);
    }
}