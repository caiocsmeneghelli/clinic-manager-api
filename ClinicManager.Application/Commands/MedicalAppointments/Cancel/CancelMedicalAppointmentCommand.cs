using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Cancel
{
    public class CancelMedicalAppointmentCommand : IRequest<Result>
    {
        public int IdAppointment { get; set; }

        public CancelMedicalAppointmentCommand(int idAppointment)
        {
            IdAppointment = idAppointment;
        }
    }
}
