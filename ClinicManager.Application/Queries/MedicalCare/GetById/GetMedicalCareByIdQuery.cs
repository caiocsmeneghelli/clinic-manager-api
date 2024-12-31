using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.MedicalCare.GetById
{
    public class GetMedicalCareByIdQuery : IRequest<Result>
    {
        public int IdMedicalCare { get; set; }

        public GetMedicalCareByIdQuery(int idMedicalCare)
        {
            IdMedicalCare = idMedicalCare;
        }
    }
}
