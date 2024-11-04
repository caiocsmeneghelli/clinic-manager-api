using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.ValueObjects
{
    public record PersonDetail
    {
        public PersonDetail(string firstName, string lastName, DateTime birthDate, 
            string phoneNumber, string email, string cPF, string bloodType)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            CPF = cPF;
            BloodType = bloodType;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string BloodType { get; private set; }
    }
}
