using Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.IdentityEntities
{
    public class CustomRole : DapperIdentityRole
    {
        //need to add custom properties to SQL table column
        //public bool IsDummy { get; set; }

        public CustomRole() { }
        public CustomRole(string roleName) : base(roleName)
        {
        }
    }
}
