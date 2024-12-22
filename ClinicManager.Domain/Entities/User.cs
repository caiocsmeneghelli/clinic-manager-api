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
        public User()
        { }
        public User(string login, string password, EProfile profile)
        {
            UserLogin = login;
            Password = password;
            Profile = profile;
            ResetPasswordRequired = true;
        }

        public string UserLogin { get; private set; }
        public string Password { get; private set; }
        public EProfile Profile { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public bool ResetPasswordRequired { get; set; }

        public void Login()
        {
            LastLogin = DateTime.Now;
        }

        public void UpdatePassword(string newPassword)
        {
            ResetPasswordRequired = false;
            Password = newPassword;
        }
    }
}
