using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using MediatR.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Create
{
    public class CreateMedicalAppointmentCommandHandler : IRequestHandler<CreateMedicalAppointmentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicalAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Validate Command
            if(request.End is null) { request.FillEnd(); }

            List<string> errors = new List<string>();
            Patient? patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);
            Doctor? doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);

            if(patient is null) { errors.Add("Paciente não encontrado."); }
            if(doctor is null) { errors.Add("Médico não encontrado."); }

            if (errors.Count > 0) { return Result.BadRequest(errors); }

            Service service = new Service(request.Service, request.Description, 
                request.Cost, request.Duration);


            return Result.Success();
        }
    }
}
