using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class userTokenRequest
    {
        public string  UserId { get; set; }
        public string  LoginProvider { get; set; }
        public string  Name { get; set; }
        public string  Value { get; set; }
    }
}
