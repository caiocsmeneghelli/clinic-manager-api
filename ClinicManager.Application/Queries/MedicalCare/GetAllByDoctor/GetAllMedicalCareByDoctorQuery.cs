using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetAllByDoctor
{
    public class GetAllMedicalCareByDoctorQuery : IRequest<Result> 
    {
        public int DoctorId { get; private set; }

        public GetAllMedicalCareByDoctorQuery(int doctorId)
        {
            DoctorId = doctorId;
        }
    }
}
