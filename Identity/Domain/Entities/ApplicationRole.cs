using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreatedDate { get; internal set; }
        public string Description { get; internal set; }
        public string IpAddress { get; internal set; }
    }
}
