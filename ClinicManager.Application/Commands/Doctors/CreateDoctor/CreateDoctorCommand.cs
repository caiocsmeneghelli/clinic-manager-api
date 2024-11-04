using ClinicManager.Application.Results;
using ClinicManager.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.CreateDoctor
{
    public class CreateDoctorCommand : IRequest<Result>
    {
        // Person Info
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string Cpf { get; set; }

        // Address
        public string Street { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string Country { get; set; }

        // Doctor
        public string CRM { get; set; }
        public string MedicalEspeciality { get; set; }

        public PersonDetail ReturnPersonDetail()
        {
            return new PersonDetail(Name, LastName, Birthdate, PhoneNumber, Email, BloodType, Cpf);
        }

        public Address ReturnAddress()
        {
            return new Address(Street, City, Uf, Country);
        }
    }
}
