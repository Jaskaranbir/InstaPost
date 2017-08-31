using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Config {
    public class Auth0Config {
        public string Domain { get; set; }
        public string ApiIdentifier { get; set; }
        public string PrivateClientId { get; set; }
        public string PrivateClientSecret { get; set; }
    }
}
