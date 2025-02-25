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

namespace ClinicManager.Application.Commands.Patients.UpdatePersonalDetail
{
    public class UpdatePatientPersonalDetailCommandHandler : IRequestHandler<UpdatePatientPersonalDetailCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePatientPersonalDetailCommand> _validator;

        public UpdatePatientPersonalDetailCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdatePatientPersonalDetailCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdatePatientPersonalDetailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) { return Result.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));}

            var patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);
            if (patient is null) { return Result.NotFound("Paciente não encontrado."); }

            var personalDetail = new PersonDetail(request.Name, request.LastName, request.Birthdate,
                request.PhoneNumber, request.Email, request.Cpf, request.BloodType);
            patient.UpdatePersonalDetail(personalDetail);

            await _unitOfWork.CompleteAsync();

            return Result.Success("Informações pessoais atualizadas.", patient);
        }
    }
}
