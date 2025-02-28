using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.Update
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateDoctorCommand> _validator;

        public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateDoctorCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if(!validationResult.IsValid)
            {
                return Result.BadRequest(validationResult.Errors.Select(reg => reg.ErrorMessage));
            }

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);
            if(doctor is null) { return Result.NotFound("Médico não encontrado."); }

            doctor.UpdateDoctor(request.CRM, request.MedicalEspeciality);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Registro salvo com sucesso.", doctor);
        }
    }
}
