using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public string phone { get; set; }
    }
}
