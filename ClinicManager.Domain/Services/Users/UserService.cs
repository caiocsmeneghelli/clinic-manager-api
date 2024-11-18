using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Enums;
using ClinicManager.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        // Criar exceção // result erro
        // Retornar nulo caso exista usuario com mesmo login.
        // Retorna novo usuário caso esteja correto
        public async Task<User?> CreateUserAndValidateLogin(string login, EProfile profile)
        {
            User? existEmail = await _unitOfWork.Users.GetByLogin(login);
            if (existEmail != null) { return null; }

            string defaultPassword = _configuration["DefaultPassword"] ?? "Password123";

            return new User(login, defaultPassword, Enums.EProfile.Doctor);
        }
    }
}
