using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Reschedule
{
    public class RescheduleMedicalAppointmentCommand : IRequest<Result>
    {
        public int IdAppointment { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
