using Api.Models;
using Api.MongoWrappers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class ReportsRepository : IReportsRepository {
        private readonly InstaPostContext db;

        // Fields Used
        private static readonly string POST_ID = "r_postId";
        private static readonly string REPORTS_ARRAY = "reports";
        private static readonly string COUNT = "r_count";
        private static readonly string IS_RESOLVED = "isResolved";

        public ReportsRepository(InstaPostContext context) {
            this.db = context;
        }

		//Adds new Report to Reports Table
        public Task<Reports> AddReportPost(int postId, int userId, string comment) {
            FilterDefinition<Reports> filter = GetPostIdFilter(postId);

            Report[] reportArray = new Report[] {new Report() {
                UserId = userId,
                Comment = comment
            }};
            Reports reports = new Reports() {
                PostId = postId,
                ReportsArray = reportArray,
                IsResolved = false
            };

            bool isEntityPresent = db.Reports.CheckAndCreateEntityBool(reports, filter);
            if(isEntityPresent) {
                Report[] initialReports = db.Reports.Find(filter).FirstOrDefault().ReportsArray;

                Task<Reports> updateTask = MongoArrayUtils<Reports>.AddToArrayWithCount<Report>(db.Reports, filter, REPORTS_ARRAY, reportArray, initialReports, COUNT);
                return updateTask;
            }
            return null;
        }

		//Sets string value to be updated in database
		//string value controls whether post is still reported
		//returns a report
        public Task<Reports> SetResolved(int postId, int userId) {
            FilterDefinition<Reports> filter = GetPostIdFilter(postId);
            UpdateDefinition<Reports> update = Builders<Reports>.Update.Set(IS_RESOLVED, true);

            Task<Reports> task = db.Reports.UpdateOneAsync(filter, update);
            return task;
        }

		//Retrieves a collection of Reported Posts
        public IEnumerable<int> GetReportedPosts(int skip = 0, int count = 10) {
            FilterDefinition<Reports> filter = Builders<Reports>.Filter.Eq(IS_RESOLVED, false);

            IEnumerable<int> array = MongoArrayUtils<Reports>.ArrayIntSplice(db.Reports, REPORTS_ARRAY, filter, count, skip);
            return array;
        }

		//Retrieves a collection of Reported Post Objects
		//Order retrieved by date and then time
        public IEnumerable<Posts> GetReportedPostObj(int skip = 0, int count = 10) {
            IEnumerable<Posts> posts = GetReportedPosts(skip, count)
                .Select(e => 
                    db.Posts.SingleOrDefault(p => p.PostId == e)
                )
                .OrderBy(e => e.PostDate)
                .OrderBy(e => e.PostTime);

            return posts;
        }

		//returns the number of times a post has been reported
        public int GetPostReportedCount(int postId) {
            int count = db.Reports.Find(e => e.PostId == postId).FirstOrDefault().Count;
            return count;
        }

		//returns a filter based on the reports of a certain post ID
        private FilterDefinition<Reports> GetPostIdFilter(int postId) {
            FilterDefinition<Reports> filter = Builders<Reports>.Filter.Eq(POST_ID, postId);
            return filter;
        }
    }
}
