using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Enums;
using ClinicManager.Domain.Services.Auth;
using ClinicManager.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;

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

            return new User(login, hashPassword, Enums.EProfile.Doctor);
        }
    }
}
