using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Enums;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Infrastructure.Auth;
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
        private readonly IAuthService _authService;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _authService = authService;
        }

        // Criar exceção // result erro
        // Retornar nulo caso exista usuario com mesmo login.
        // Retorna novo usuário caso esteja correto
        public async Task<User?> CreateUserAndValidateLogin(string login, EProfile profile)
        {
            User? existEmail = await _unitOfWork.Users.GetByLogin(login);
            if (existEmail != null) { return null; }

            string defaultPassword = _configuration["DefaultPassword"] ?? "Password123";
            string hashPassword = _authService.ComputeSha256Hash(defaultPassword);

            return new User(login, defaultPassword, Enums.EProfile.Doctor);
        }
    }
}
