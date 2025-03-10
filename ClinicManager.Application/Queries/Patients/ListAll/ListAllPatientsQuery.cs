using ClinicManager.Application.Helpers;
using ClinicManager.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.Patients.ListAll
{
    public class ListAllPatientsQuery : IRequest<PageList<PatientViewModel>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
