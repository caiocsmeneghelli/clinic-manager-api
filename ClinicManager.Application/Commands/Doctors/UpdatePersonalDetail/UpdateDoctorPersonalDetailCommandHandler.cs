using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.UpdatePersonalDetail
{
    public class UpdateDoctorPersonalDetailCommandHandler : IRequestHandler<UpdateDoctorPersonalDetailCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateDoctorPersonalDetailCommand> _validator;

        public UpdateDoctorPersonalDetailCommandHandler(IUnitOfWork unitOfWork, 
            IValidator<UpdateDoctorPersonalDetailCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateDoctorPersonalDetailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) { return Result.BadRequest(validationResult.Errors.Select(reg => reg.ErrorMessage)); }

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);
            if(doctor is null) { return Result.NotFound("Médico não encontrado."); }

            PersonDetail personalDetail = new PersonDetail(request.Name, request.LastName, request.Birthdate,
                request.PhoneNumber, request.Email, request.Cpf, request.BloodType);

            doctor.UpdatePersonalDetail(personalDetail);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Informações pessoais atualizadas.", doctor);
        }
    }
}
