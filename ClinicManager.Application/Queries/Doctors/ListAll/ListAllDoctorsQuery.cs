using ClinicManager.Application.Helpers;
using ClinicManager.Application.ViewModel;
using MediatR;

namespace ClinicManager.Application.Queries.Doctors.ListAll
{
    public class ListAllDoctorsQuery : IRequest<PageList<DoctorViewModel>>
    {
        public PageParams PageParams { get; set; }

        public ListAllDoctorsQuery(PageParams pageParams)
        {
            PageParams = pageParams;
        }
    }
}
