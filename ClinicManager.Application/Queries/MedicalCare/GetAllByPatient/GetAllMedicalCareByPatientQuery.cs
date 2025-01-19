using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetAllByPatient
{
    public class GetAllMedicalCareByPatientQuery : IRequest<Result>
    {
        public int PatientId { get; private set; }

        public GetAllMedicalCareByPatientQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
