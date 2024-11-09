using ClinicManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class User : Entity
    {
        public User(string login, string password, EProfile profile)
        {
            Login = login;
            Password = password;
            Profile = profile;
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
        public EProfile Profile { get; private set; }
        public DateTime? LastLogin { get; private set; }

        public void Login()
        {
            LastLogin = DateTime.Now;
        }
    }
}
