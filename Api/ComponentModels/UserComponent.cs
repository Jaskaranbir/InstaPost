using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ComponentModels
{
    public class UserComponent
    {
        public int UserId { get; }

        public string FirstName { get; }
        public string LastName { get; }
        public string ProfilePicture { get; }
        public string Usertag { get; }

        public UserComponent(Users user) {
            this.UserId = user.UserId;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.ProfilePicture = user.ProfilePicture;
            this.Usertag = user.Usertag;
        }

    }
}
