using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalAppointments.GetAllByPatient
{
    public class GetAllMedicalAppointmentsByPatientQuery : IRequest<Result>
    {
        public int PatientId { get; private set; }

        public GetAllMedicalAppointmentsByPatientQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
