using ClinicManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModel
{
    public class DoctorViewModel
    {
        public PersonDetailViewModel PersonDetail { get; set; }
        public AddressViewModel Address { get; set; }
        public string MedicalSpecialty { get; set; }
        public string CRM { get; set; }
        public string UserName { get; set; }
    }
}
