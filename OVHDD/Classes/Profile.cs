using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVHDD.Classes
{
    public class Profile
    {
        public string name { get; set; }
        public string host { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public Profile(string name, string host, string username, string password)
        {
            this.name = name;
            this.host = host;
            this.username = username;
            this.password = password;
        }
    }
}
