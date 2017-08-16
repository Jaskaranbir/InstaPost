using Api.Models;
using Api.MongoWrappers;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class TagsRepository : ITagsRepository {
        private readonly InstaPostContext db;

        // Fields Used
        private static readonly string TAG = "tag";
        private static readonly string POST_IDS = "t_postIds";

        public TagsRepository(InstaPostContext context) {
            this.db = context;
        }

		//Adds a new tag to Tags Table
        private Task<Tags> AddTag(string tagText, params int[] postIds) {
            Tags tag = new Tags() {
                Tag = tagText,
                PostIds = postIds
            };
            FilterDefinition<Tags> filter = GetTagTextFilter(tagText);

            bool isEntityPresent = db.Tags.CheckAndCreateEntityBool(tag, filter);
            if (isEntityPresent) {
                Task<Tags> task = MongoArrayUtils<Tags>.AddToArray<int>(db.Tags, filter, POST_IDS, postIds);
                return task;
            }
            return null;
        }
        
		//returns an array of tasks that have been added to a post
        public Task<Tags>[] AddPostTags(string[] tagTexts, int postId) {
            Task<Tags>[] tasks = new Task<Tags>[tagTexts.Length];

            for(int i = 0; i < tasks.Length; i++)
                tasks[i] = AddTag(tagTexts[i], postId);

            return tasks;
        }

		//Removes tag from Tags table
        public Task<Tags> RemoveTag(string tagText, int postId) {
            FilterDefinition<Tags> filter = GetTagTextFilter(tagText);

            Task<Tags> task = MongoArrayUtils<Tags>.RemoveFromArray<int>(db.Tags, filter, POST_IDS, new int[] { postId });

            return task;
        }

		//returns a collection of PostID's that contain tag
        public IEnumerable<int> GetPostsByTag(string tagText, int count = 10, int skip = 0) {
            FilterDefinition<Tags> filter = GetTagTextFilter(tagText);

            IEnumerable<int> array = MongoArrayUtils<Tags>.ArrayIntSplice(db.Tags, POST_IDS, filter, count, skip);
            return array;
        }
		
		//returns a collection of Post objects that contain tag
        public IEnumerable<Posts> GetPostsObjByTag(string tagText, int count = 10, int skip = 0) {
            IEnumerable<Posts> posts = GetPostsByTag(tagText, count, skip)
                .Select(e =>
                    db.Posts.SingleOrDefault(p => p.PostId == e)
                );

            return posts;
        }

		//returns the amount of posts that have a specific tag
        public long GetPostCountByTag(string tagText) {
            long count = db.Tags.Find(e => e.Tag == tagText).Count();
            return count;
        }

		//returns a filter for tags
        public FilterDefinition<Tags> GetTagTextFilter(string tagText) {
            FilterDefinition<Tags> filter = Builders<Tags>.Filter.Eq(TAG, tagText);
            return filter;
        }
    }
}
