using Api.Models;
using Api.ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        InstaPostContext db;
        public PostsController(InstaPostContext context) {
            db = context;
        }
        // IConfiguration _iconfiguration;

        // public ValuesController(IConfiguration iconfiguration)
        // {
        //   _iconfiguration = iconfiguration;
        // }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            // Get the post. This time we are just selecting whatever
            // first post we can get. But it will be based on some parameters
            // or filters in real application.
            var post = db.Posts.Where(e => true).FirstOrDefault();

            // Get user and location associated with post
            var user = db.Users.Where(e => e.UserId == post.UserId).FirstOrDefault();
            var location = db.Locations.Where(e => e.PostId == post.PostId).FirstOrDefault();

            // This is our ComponentModel (contains all the necessary data to tarnsfer over network)
            var PostObj = new Post(post, user, location);

            // Convert class obj to JSON
            return JsonConvert.SerializeObject(PostObj);

            /*
                Base Idea is to form the object ith all required data
                and serialize it using JsonConvert.
             */
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    // Ignore this for now, too
    // public class Customer
    // {
    //   public ObjectId _id { get; set; }
    //   public string firstName { get; set; }
    //   public string lastName { get; set; }

    //   public override string ToString()
    //   {
    //       return "Person: " + firstName + " " + lastName;
    //   }
    // }
}
