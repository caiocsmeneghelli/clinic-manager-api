using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Services.Users;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValidator<CreateDoctorCommand> _validator;

        public CreateDoctorCommandHandler(IUnitOfWork unitOfWork,
            IValidator<CreateDoctorCommand> validator,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _userService = userService;
        }

        public async Task<Result> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid == false) {
                List<string> errors = validationResult.Errors
                        .Select(reg => reg.ErrorMessage)
                        .ToList();
                return Result.BadRequest(errors);
            }

            await _unitOfWork.BeginTransaction();

            var user = await _userService.CreateUserAndValidateLogin(request.UserLogin, Domain.Enums.EProfile.Doctor);
            if(user is null) { return Result.BadRequest("Login de usuário ja existente."); }

            int idUser = await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.CompleteAsync();

            PersonDetail personDetail = request.ReturnPersonDetail();
            Address address = request.ReturnAddress();
            var doctor = new Doctor(request.MedicalEspeciality, request.CRM, user.Id, personDetail, address);

            await _unitOfWork.Doctors.CreateAsync(doctor);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
