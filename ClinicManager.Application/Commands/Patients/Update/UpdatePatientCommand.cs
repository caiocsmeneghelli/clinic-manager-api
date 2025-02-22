using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.Update
{
    public class UpdatePatientCommand : IRequest<Result>
    {
        public int IdPatient { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
    }
}
