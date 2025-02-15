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

namespace ClinicManager.Application.Commands.Doctors.UpdateAddress
{
    public class UpdateDoctorAddressCommandHandler : IRequestHandler<UpdateDoctorAddressCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateDoctorAddressCommand> _validator;

        public UpdateDoctorAddressCommandHandler(IValidator<UpdateDoctorAddressCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDoctorAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if(!validationResult.IsValid) { return Result.BadRequest(validationResult.Errors.Select(v => v.ErrorMessage)); }

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);
            if (doctor is null) { return Result.NotFound("Médico não encontrado."); }

            Address address = new Address(request.Street, request.City, request.Uf, request.Country);
            doctor.UpdateAddress(address);

            await _unitOfWork.CompleteAsync();

            return Result.Success(doctor);
        }
    }
}
