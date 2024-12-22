using ClinicManager.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<Result>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string UserName { get; set; }
    }
}
