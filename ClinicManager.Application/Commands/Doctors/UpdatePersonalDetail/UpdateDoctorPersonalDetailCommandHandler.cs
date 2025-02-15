using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Domain.ValueObjects;
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

        public UpdateDoctorPersonalDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDoctorPersonalDetailCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);
            if(doctor is null) { return Result.NotFound("Médico não encontrado."); }

            PersonDetail personalDetail = new PersonDetail(request.Name, request.LastName, request.Birthdate,
                request.PhoneNumber, request.Email, request.Cpf, request.BloodType);

            doctor.UpdatePersonalDetail(personalDetail);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Informações pessoais atualizadas.");
        }
    }
}
