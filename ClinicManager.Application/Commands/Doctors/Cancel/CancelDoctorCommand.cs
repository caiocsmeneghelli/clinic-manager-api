using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.Cancel
{
    public class CancelDoctorCommand : IRequest<Result>
    {
        public int IdDoctor { get; private set; }

        public CancelDoctorCommand(int idDoctor)
        {
            IdDoctor = idDoctor;
        }
    }
}
