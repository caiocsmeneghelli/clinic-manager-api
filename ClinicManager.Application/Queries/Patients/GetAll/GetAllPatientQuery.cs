using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.GetAll
{
    public class GetAllPatientQuery : IRequest<Result>
    {
    }
}
