using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModel
{
    public class AddressViewModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public string Country { get; set; }

        public string FullAddress { get; set; }
    }
}
