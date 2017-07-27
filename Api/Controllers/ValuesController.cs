using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    InstaPostContext db;
    public ValuesController(InstaPostContext context) {
      db = context;
    }
    // IConfiguration _iconfiguration;

    // public ValuesController(IConfiguration iconfiguration)
    // {
    //   _iconfiguration = iconfiguration;
    // }

    // GET api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
      var usersTable = db.Users;
      // Just select any user (basically selects all users), and display whatever is the first result.
      var user = usersTable.Where(e => true).FirstOrDefault();

        // Ignore below stuff for now.....

      // string constr2 = _iconfiguration.GetValue<string>("ConnectionStrings:InstaPostDB_Mongo");

      // var mongoc = new MongoClient(constr2).GetDatabase("test").GetCollection<Customer>("customers");

      // var collection = mongoc.GetCollection<BsonDocument>("instapost");
      // var filter = new BsonDocument();


      // var query = Query.EQ("Title","First post!");
      // var cursor = collection;
      // x += "            " + mongoc.Find(m => true).First().firstName;

      return new string[] { user.Email };
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
