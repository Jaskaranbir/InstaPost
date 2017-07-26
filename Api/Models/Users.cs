﻿using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Locations = new HashSet<Locations>();
            Posts = new HashSet<Posts>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emails { get; set; }
        public string Passwords { get; set; }
        public string Suspended { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
