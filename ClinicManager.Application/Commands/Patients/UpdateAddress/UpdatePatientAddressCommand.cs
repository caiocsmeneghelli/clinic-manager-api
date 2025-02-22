using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Patients.UpdateAddress
{
    public class UpdatePatientAddressCommand : IRequest<Result>
    {
        public int IdPatient { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string Country { get; set; }
    }
}
