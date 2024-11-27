using ClinicManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModel
{
    public class LoginViewModel
    {
        public string Token { get; set; }
        public EProfile Profile { get; set; }
    }
}
