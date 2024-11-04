using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
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
        private readonly IValidator<CreateDoctorCommand> _validator;

        public CreateDoctorCommandHandler(IUnitOfWork unitOfWork, 
            IValidator<CreateDoctorCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
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

            PersonDetail personDetail = request.ReturnPersonDetail();
            Address address = request.ReturnAddress();
            var doctor = new Doctor(personDetail, address, request.MedicalEspeciality, request.CRM);

            await _unitOfWork.Doctors.CreateAsync(doctor);
            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}
