using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
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
        private readonly IValidator<RescheduleMedicalAppointmentCommand> _validator;

        public RescheduleMedicalAppointmentCommandHandler(IUnitOfWork unitOfWork,
            IValidator<RescheduleMedicalAppointmentCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(RescheduleMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(reg => reg.ErrorMessage);
                return Result.BadRequest(errors);
            }

            var medicalAppointment = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByIdAsync(request.IdAppointment);
            if (medicalAppointment == null) { return Result.NotFound("Atendimento não encontrado."); }

            var medicalAppointmentExists = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByDoctorAndDate(medicalAppointment.IdDoctor, request.Start);
            if (medicalAppointmentExists != null) { return Result.BadRequest("Médico possui consulta nesse horário."); }

            medicalAppointment.Reschedule(request.Start, request.End);
            await _unitOfWork.CompleteAsync();

            return Result.Success("Atendimento reagendado.");
        }
    }
}
