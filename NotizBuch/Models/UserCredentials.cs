using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotizBuch.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentials(string user, string password)
        {
            Username = user;
            Password = password;
        }

        public UserCredentials()
        {

        }

        internal string EncryptPassword()
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(Password))).Replace("-", "");
            }
        }

    }
}
