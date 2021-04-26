using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mk4.Security
{
    public class AppIdentityRole    : IdentityRole
    {
        //public bool Admin { get; set; }
        public string RoleDescription { get; set; }
    }
}
