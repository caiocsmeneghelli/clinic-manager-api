using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
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
            // Validate
            var personInfo = request.ReturnPersonDetail();
            var address = request.ReturnAddress();
            var doctor = new Doctor(personInfo, address, request.MedicalEspeciality, request.CRM);

            return Result.Success();
        }
    }
}
