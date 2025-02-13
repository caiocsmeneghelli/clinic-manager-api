using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Cancel
{
    public class CancelMedicalAppointmentCommandHandler : IRequestHandler<CancelMedicalAppointmentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelMedicalAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByIdAsync(request.IdAppointment);

            if (medicalAppointment == null) { return Result.NotFound("Atendimento não encontrado."); }

            var service = await _unitOfWork.Services
                .GetByIdAsync(medicalAppointment.Id);
            if (service == null) { return Result.Failure((int)HttpStatusCode.InternalServerError,
                "Não existe Serviço vinculado ao Atendimento."); }

            await _unitOfWork.BeginTransaction();

            medicalAppointment.Cancel();
            await _unitOfWork.CompleteAsync();

            service.Cancel();
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success("Atendimento cancelado.");
        }
    }
}
