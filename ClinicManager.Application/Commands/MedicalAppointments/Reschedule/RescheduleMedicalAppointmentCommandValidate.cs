using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Reschedule
{
    public class RescheduleMedicalAppointmentCommandValidate : AbstractValidator<RescheduleMedicalAppointmentCommand>
    {
        public RescheduleMedicalAppointmentCommandValidate()
        {
            RuleFor(reg => reg.IdAppointment).NotEmpty().WithMessage("Consulta não pode ser vazia.");
            RuleFor(reg => reg.Start).Must(StartMustBeInFuture)
                .WithMessage("Data de inicio da consulta não pode ser no passado.");

            When(reg => reg.End != null, () =>
            {
                RuleFor(reg => reg).Must(EndMustBeGreaterThanStart)
                    .WithMessage("Data de termino da consulta precisa ser depois da data de inicio.");
            });
        }

        private bool StartMustBeInFuture(DateTime start)
        {
            return start > DateTime.Now;
        }

        private bool EndMustBeGreaterThanStart(RescheduleMedicalAppointmentCommand command)
        {
            return command.End.Value > command.Start;
        }
    }
}
