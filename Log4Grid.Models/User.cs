using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
    }
}
