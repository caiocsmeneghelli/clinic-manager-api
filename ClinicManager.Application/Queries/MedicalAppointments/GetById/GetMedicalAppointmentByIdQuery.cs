using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalAppointments.GetById
{
    public class GetMedicalAppointmentByIdQuery : IRequest<Result>
    {
        public int IdMedicalCare { get; set; }

        public GetMedicalAppointmentByIdQuery(int idMedicalCare)
        {
            IdMedicalCare = idMedicalCare;
        }
    }
}
