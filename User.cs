using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    class User
    {
        private string username;
        private int userPoints;

        public User(string username, int userPoints)
        {
            this.username = username;
            this.userPoints = userPoints;
        }

        public string Username { get => username; set => username = value; }
        public int UserPoints { get => userPoints; set => userPoints = value; }
    }
}
