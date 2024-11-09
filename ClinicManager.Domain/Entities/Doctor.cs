using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Doctor : Entity
    {
        public Doctor(PersonDetail personDetail, Address address, string medicalSpecialty, string crm, int idUser)
        {
            PersonDetail = personDetail;
            Address = address;
            MedicalSpecialty = medicalSpecialty;
            CRM = crm;
            IdUser = idUser;
        }

        public PersonDetail PersonDetail { get; private set; }
        public Address Address { get; private set; }
        public string MedicalSpecialty { get; private set; }
        public string CRM { get; private set; }
        public List<MedicalCare> MedicalCares { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
    }
}
