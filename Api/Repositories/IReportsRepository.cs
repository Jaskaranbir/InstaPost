using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories {
	//Interface that defines the methods used to Report and keep track of a Reported user 
    public interface IReportsRepository {
		//Retrieves the number of times said post has been reported
        int GetPostReportedCount(int postId);
		
		//Adds a user's post to the Reports table 
		Task<Reports> AddReportPost(int postId, int userId, string comment);
        
		//Retrieves a Reported Post Object from Reports table
		IEnumerable<Posts> GetReportedPostObj(int skip = 0, int count = 10);
        
		//Retrieves a collection of reported posts
		IEnumerable<int> GetReportedPosts(int skip = 0, int count = 10);
        
		//Sets the status of a Reported Post to Resolved, meaning it is no longer reported
		Task<Reports> SetResolved(int postId, int userId);
    }
}