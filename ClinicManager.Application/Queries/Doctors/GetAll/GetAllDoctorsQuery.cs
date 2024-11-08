using ClinicManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Doctors.GetAll
{
    public class GetAllDoctorsQuery : IRequest<List<Doctor>>
    {
    }
}
