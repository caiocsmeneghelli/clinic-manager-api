﻿using ClinicManager.Application.Results;
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

namespace ClinicManager.Application.Commands.Patients.CreatePatient
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePatientCommand> _validator;

        public CreatePatientCommandHandler(IUnitOfWork unitOfWork, IValidator<CreatePatientCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid == false)
            {
                List<string> errors = validationResult.Errors
                        .Select(reg => reg.ErrorMessage)
                        .ToList();
                return Result.BadRequest(errors);
            }

            PersonDetail personDetail = request.ReturnPersonDetail();
            Address address = request.ReturnAddress();
            var patient = new Patient(request.Height, request.Weight, personDetail, address);

            await _unitOfWork.Patients.CreateAsync(patient);
            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}
