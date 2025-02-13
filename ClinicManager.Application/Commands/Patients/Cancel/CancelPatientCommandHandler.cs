using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.Cancel
{
    public class CancelPatientCommandHandler : IRequestHandler<CancelPatientCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelPatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);

            if (patient == null) { return Result.NotFound("Paciente não encontrado."); }

            var medicalAppointments = await _unitOfWork.MedicalAppointments
               .GetAllByPatient(request.IdPatient);

            var services = await _unitOfWork.Services.GetAllByPatient(request.IdPatient);

            await _unitOfWork.BeginTransaction();

            patient.Cancel();
            await _unitOfWork.CompleteAsync();

            foreach (var medicalAppointment in medicalAppointments) { medicalAppointment.Cancel(); }
            await _unitOfWork.CompleteAsync();

            foreach (var service in services) { service.Cancel(); }
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success("O registro Paciente foi cancelado.");
        }
    }
}
