using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Cancel
{
    public class CancelMedicalAppointmentCommandQuery : IRequestHandler<CancelMedicalAppointmentCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CancelMedicalAppointmentCommandQuery(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByIdAsync(request.IdAppointment);

            if (medicalAppointment == null) { return Result.NotFound("Atendimento não encontrado."); }

            await _unitOfWork.BeginTransaction();

            medicalAppointment.Cancel();
            await _unitOfWork.CompleteAsync();

            medicalAppointment.Service.Cancel();
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success("Atendimento cancelado.");
        }
    }
}
