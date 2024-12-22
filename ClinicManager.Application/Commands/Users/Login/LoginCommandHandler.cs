using ClinicManager.Application.Results;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Services.Auth;
using ClinicManager.Domain.Services.Users;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string hash = _authService.ComputeSha256Hash(request.Password);
            User? user = await _unitOfWork.Users.GetUserByEmailPassword(request.Email, hash);
            if (user == null) { return Result.BadRequest("Usuário ou senha incorreta."); }

            user.Login();
            await _unitOfWork.CompleteAsync();
            var token = _authService.GenerateJwtToken(user.UserLogin, user.Profile.ToString());
            LoginViewModel viewModel = new LoginViewModel()
            {
                Profile = user.Profile,
                Token = token,
                ResetPasswordRequired = user.ResetPasswordRequired
            };

            return Result.Success(viewModel);
        }
    }
}
