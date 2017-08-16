using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using MongoDB.Driver;

namespace Api.Repositories {
    public interface ITagsRepository {
        //Adds a Tag to an associated Post
		Task<Tags>[] AddPostTags(string[] tagTexts, int postId);
        
		//Returns amount of posts that are tagged with the associated string
		long GetPostCountByTag(string tagText);
        
		//Retrieves a collection of Posts based on having the in-putted tag
		IEnumerable<int> GetPostsByTag(string tagText, int count = 10, int skip = 0);
        
		//Retrieves a Post Object based on having the in-putted tag
		IEnumerable<Posts> GetPostsObjByTag(string tagText, int count = 10, int skip = 0);
        
		//Returns a filter definition for tags
		FilterDefinition<Tags> GetTagTextFilter(string tagText);
        
		//Allows for tag to removed/disassociated with a post 
		Task<Tags> RemoveTag(string tagText, int postId);
    }
}