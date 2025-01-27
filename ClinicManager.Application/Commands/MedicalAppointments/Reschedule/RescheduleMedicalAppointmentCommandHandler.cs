using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Reschedule
{
    public class RescheduleMedicalAppointmentCommandHandler : IRequestHandler<RescheduleMedicalAppointmentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RescheduleMedicalAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RescheduleMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByIdAsync(request.IdAppointment);
            if (medicalAppointment == null) { return Result.NotFound("Atendimento não encontrado."); }

            medicalAppointment.Reschedule(request.Start, request.End);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Atendimento reagendado.");
        }
    }
}
