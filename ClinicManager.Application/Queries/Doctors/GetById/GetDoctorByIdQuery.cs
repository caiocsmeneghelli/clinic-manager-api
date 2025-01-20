using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.GetById
{
    public class GetDoctorByIdQuery : IRequest<Result>
    {
        public int IdDoctor { get; private set; }

        public GetDoctorByIdQuery(int idDoctor)
        {
            IdDoctor = idDoctor;
        }
    }
}
