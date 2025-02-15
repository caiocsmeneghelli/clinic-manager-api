using ClinicManager.Application.Results;
using ClinicManager.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.Update
{
    public class UpdateDoctorCommand : IRequest<Result>
    {
        // Doctor
        public int IdDoctor { get; set; }
        public string CRM { get; set; }
        public string MedicalEspeciality { get; set; }
    }
}
