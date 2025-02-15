using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.Update
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
    }
}
