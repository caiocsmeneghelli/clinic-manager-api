using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Doctor : Entity
    {
        public Doctor()
        { }

        public Doctor(string medicalSpecialty, string crm, int idUser,
            PersonDetail personDetail, Address address)
        {
            MedicalSpecialty = medicalSpecialty;
            CRM = crm;
            IdUser = idUser;
            PersonDetail = personDetail;
            Address = address;
        }

        public PersonDetail PersonDetail { get; private set; }
        public Address Address { get; private set; }
        [MaxLength(128)]
        public string MedicalSpecialty { get; private set; }
        [StringLength(13)]
        [Required]
        public string CRM { get; private set; }
        public List<MedicalAppointment> MedicalAppointments { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
    }
}
