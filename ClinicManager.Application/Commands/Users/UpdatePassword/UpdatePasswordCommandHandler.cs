﻿using ClinicManager.Application.Results;
using ClinicManager.Domain.Services.Auth;
using ClinicManager.Domain.UnitOfWork;
using MediatR;

namespace ClinicManager.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public UpdatePasswordCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            string hashOldPassword = _authService.ComputeSha256Hash(request.OldPassword);
            string hashNewPassword = _authService.ComputeSha256Hash(request.NewPassword);

            var user = await _unitOfWork.Users.GetUserByEmailPassword(request.UserName, hashOldPassword);
            if(user is null)
            {
                return Result.BadRequest("Senha antiga não confere.");
            }

            user.UpdatePassword(hashNewPassword);
            await _unitOfWork.CommitAsync();

            // Refresh Token?

            return Result.Success(user);
        }
    }
}
