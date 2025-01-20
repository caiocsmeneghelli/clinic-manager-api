using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalAppointments.GetAllByDoctor
{
    public class GetAllMedicalAppointmentsByDoctorQuery : IRequest<Result> 
    {
        public int DoctorId { get; private set; }

        public GetAllMedicalAppointmentsByDoctorQuery(int doctorId)
        {
            DoctorId = doctorId;
        }
    }
}
