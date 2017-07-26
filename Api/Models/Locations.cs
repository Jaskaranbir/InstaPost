using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Locations
    {
        public int Locationid { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }
        public int Postid { get; set; }

        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
    }
}
