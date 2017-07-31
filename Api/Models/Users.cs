using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Users
    {
        public Users()
        {
            Administrators = new HashSet<Administrators>();
            Comments = new HashSet<Comments>();
            Locations = new HashSet<Locations>();
            Posts = new HashSet<Posts>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Usertag { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSuspended { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<Administrators> Administrators { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
