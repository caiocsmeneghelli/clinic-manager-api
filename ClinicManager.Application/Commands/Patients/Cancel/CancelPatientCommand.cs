using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.Cancel
{
    public class CancelPatientCommand : IRequest<Result>
    {
        public CancelPatientCommand(int idPatient)
        {
            IdPatient = idPatient;
        }

        public int IdPatient { get; set; }
    }
}
