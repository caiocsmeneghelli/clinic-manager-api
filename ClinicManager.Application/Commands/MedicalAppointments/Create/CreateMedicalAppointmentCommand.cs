using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.MedicalAppointments.Create
{
    public class CreateMedicalAppointmentCommand : IRequest<Result>
    {
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public string HealthPlan { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public string Service { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }

        public void FillEnd()
        {
            End = Start.AddMinutes(Duration);
        }
    }
}
