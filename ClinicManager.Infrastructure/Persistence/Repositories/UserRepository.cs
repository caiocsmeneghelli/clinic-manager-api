using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClinicManagerDbContext _context;

        public UserRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user.Id;
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.SingleOrDefaultAsync(reg => reg.UserLogin.Equals(login));
        }

        public async Task<User?> GetUserByEmailPassword(string email, string hashPassword)
        {
            return await _context.Users
                .Where(u => u.UserLogin.Equals(email))
                .Where(u => u.Password.Equals(hashPassword))
                .SingleOrDefaultAsync();
        }
    }
}
