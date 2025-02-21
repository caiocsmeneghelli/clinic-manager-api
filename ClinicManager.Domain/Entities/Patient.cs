using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Patient : Entity
    {
        public Patient()
        { }
        public Patient(decimal height, decimal weight, PersonDetail personDetail, Address address, int idUser)
        {
            Height = height;
            Weight = weight;
            PersonDetail = personDetail;
            Address = address;
            IdUser = idUser;
        }

        [Required]
        [Column(TypeName ="decimal(5,2)")]
        public decimal Height { get; private set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; private set; }
        public PersonDetail PersonDetail { get; private set; }
        public Address Address { get; private set; }
        public List<MedicalAppointment> MedicalAppointments { get; set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }

        public void UpdatePersonalDetail(PersonDetail personal)
        {
            PersonDetail = personal;
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}
