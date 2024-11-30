using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModel
{
    public class PersonDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Cpf { get; set; }
        public string BloodType { get; set; }
    }
}
