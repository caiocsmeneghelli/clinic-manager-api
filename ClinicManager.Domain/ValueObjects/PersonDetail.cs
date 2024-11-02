using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.ValueObjects
{
    public record PersonDetail
    {
        public PersonDetail(string name, string surname, DateTime birthDate, 
            string phoneNumber, string email, string cPF, string bloodType)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string BloodType { get; private set; }
    }
}
