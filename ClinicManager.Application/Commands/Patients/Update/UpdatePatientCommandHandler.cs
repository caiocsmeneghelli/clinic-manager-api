using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.Update
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePatientCommand> _validator;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdatePatientCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if(!validationResult.IsValid) { return Result.BadRequest(validationResult.Errors.Select(reg => reg.ErrorMessage)); }

            var patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);
            if(patient is null) { return Result.NotFound("Paciente não encontrado."); }

            patient.Update(request.Height, request.Weight);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Registro de paciente atualizado com sucesso.", patient);
        }
    }
}
