using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModel
{
    public class PatientViewModel
    {
        public PersonDetailViewModel PersonDetail { get; set; }
        public AddressViewModel Address { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Username { get; set; }
    }
}
