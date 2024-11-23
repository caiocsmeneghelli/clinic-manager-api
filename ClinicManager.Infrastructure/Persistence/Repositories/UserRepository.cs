using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<int> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}
