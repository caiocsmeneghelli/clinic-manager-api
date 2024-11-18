using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Services.Users
{
    public interface IUserService
    {
        Task<User?> CreateUserAndValidateLogin(string login, EProfile profile);

    }
}
