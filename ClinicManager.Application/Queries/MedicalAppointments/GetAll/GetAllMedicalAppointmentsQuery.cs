using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetAll
{
    public class GetAllMedicalAppointmentsQuery : IRequest<Result>
    {
    }
}
