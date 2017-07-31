using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Administrators
    {
        public int AdministratorId { get; set; }
        public int? UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
