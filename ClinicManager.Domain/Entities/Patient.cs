using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
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

        public decimal Height { get; private set; }
        public decimal Weight { get; private set; }
        public PersonDetail PersonDetail { get; private set; }
        public Address Address { get; private set; }
        //public List<MedicalCare> MedicalCares { get; set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
    }
}
