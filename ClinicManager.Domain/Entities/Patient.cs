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
        public Patient(decimal height, decimal weight, PersonDetail personDetail, Address address)
        {
            Height = height;
            Weight = weight;
            PersonDetail = personDetail;
            Address = address;
        }

        public decimal Height { get; private set; }
        public decimal Weight { get; private set; }
        public PersonDetail PersonDetail { get; private set; }
        public Address Address { get; private set; }
        public List<MedicalCare> MedicalCares { get; set; }
    }
}
