using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Users
    {
		//Instantiates the following classes within a User object to allow a user to contain the following information
        public Users()
        {
            Administrators = new HashSet<Administrators>();
            Comments = new HashSet<Comments>();
            Locations = new HashSet<Locations>();
            Posts = new HashSet<Posts>();
        }

        public int UserId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        public string Usertag { get; set; }
        public bool IsSuspended { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        public virtual ICollection<Administrators> Administrators { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
