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

namespace ClinicManager.Application.Commands.Patients.UpdateAddress
{
    public class UpdatePatientAddressCommandHandler : IRequestHandler<UpdatePatientAddressCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePatientAddressCommand> _validator;

        public UpdatePatientAddressCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdatePatientAddressCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdatePatientAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) { return Result.BadRequest(validationResult.Errors.Select(reg => reg.ErrorMessage)); }

            var patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);
            if(patient is null) { return Result.NotFound("Paciente não encontrado."); }

            var address = new Address(request.Street, request.City, request.Uf, request.Country);
            patient.UpdateAddress(address);

            await _unitOfWork.CompleteAsync();

            return Result.Success("Endereço atualizado.");
        }
    }
}
