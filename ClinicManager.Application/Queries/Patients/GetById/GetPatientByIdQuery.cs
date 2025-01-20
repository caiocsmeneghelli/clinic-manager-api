using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.GetById
{
    public class GetPatientByIdQuery : IRequest<Result>
    {
        public int IdPatient { get; set; }

        public GetPatientByIdQuery(int idPatient)
        {
            IdPatient = idPatient;
        }
    }
}
