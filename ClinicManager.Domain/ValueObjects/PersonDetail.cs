using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [MaxLength(256)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; private set; }
        
        [Required]
        public DateTime BirthDate { get; private set; }

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; private set; }

        [Required]
        [MaxLength(64)]
        public string BloodType { get; private set; }
    }
}
